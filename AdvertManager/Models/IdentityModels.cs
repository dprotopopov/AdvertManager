using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyLibrary.Collections;
using RT.Domain.Models;

namespace AdvertManager.Models
{
    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            ClaimsIdentity userIdentity =
                await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class FakeDbContext : IdentityDbContext
    {
        private static ObservableCollection<RT.Domain.Models.Action> actionDbSet =
            new ObservableCollection<RT.Domain.Models.Action>();

        private static ObservableCollection<RT.Domain.Models.Rubric> rubricDbSet =
            new ObservableCollection<RT.Domain.Models.Rubric>();

        private static ObservableCollection<RT.Domain.Models.Region> regionDbSet =
            new ObservableCollection<RT.Domain.Models.Region>();

        private static readonly ObservableCollection<RT.Domain.Models.Action> actionRootSet =
            new ObservableCollection<RT.Domain.Models.Action>();

        private static readonly ObservableCollection<RT.Domain.Models.Rubric> rubricRootSet =
            new ObservableCollection<RT.Domain.Models.Rubric>();

        private static readonly ObservableCollection<RT.Domain.Models.Region> regionRootSet =
            new ObservableCollection<RT.Domain.Models.Region>();

        private static ObservableCollection<RubricCost> costDbSet =
            new ObservableCollection<RubricCost>();

        private static readonly StackListQueue<Record> modified = new StackListQueue<Record>();
        private static readonly StackListQueue<Record> deleted = new StackListQueue<Record>();

        public FakeDbContext()
        {
            Refresh();
        }

        public StackListQueue<Record> Modified
        {
            get { return modified; }
        }

        public StackListQueue<Record> Deleted
        {
            get { return deleted; }
        }

        public ObservableCollection<RT.Domain.Models.Region> RegionRootSet
        {
            get { return regionRootSet; }
        }

        public ObservableCollection<RT.Domain.Models.Rubric> RubricRootSet
        {
            get { return rubricRootSet; }
        }

        public ObservableCollection<RT.Domain.Models.Action> ActionRootSet
        {
            get { return actionRootSet; }
        }

        public ObservableCollection<RT.Domain.Models.Action> ActionDbSet
        {
            get { return actionDbSet; }
        }

        public ObservableCollection<RT.Domain.Models.Rubric> RubricDbSet
        {
            get { return rubricDbSet; }
        }

        public ObservableCollection<RubricCost> CostDbSet
        {
            get { return costDbSet; }
        }

        public ObservableCollection<RT.Domain.Models.Region> RegionDbSet
        {
            get { return regionDbSet; }
        }

        public ObservableCollection<RubricCost> RubricCostDbSet { get; set; }

        public new int SaveChanges()
        {
            foreach (RT.Domain.Models.Rubric rubric in rubricDbSet)
                foreach (RubricCost cost in rubric.Costs)
                    cost.RubricId = rubric.Id;

            foreach (Record record in modified) AdvertManager.Database.Persistent.InsertOrReplace(record);
            foreach (Record record in deleted) AdvertManager.Database.Persistent.Delete(record);

            modified.Clear();
            deleted.Clear();

            Refresh();

            return base.SaveChanges();
        }

        public void Repare()
        {
            Refresh();

            Dictionary<int, RT.Domain.Models.Action> actions1 = actionDbSet.ToDictionary(item => item.Id, item => item);
            Dictionary<int, RT.Domain.Models.Rubric> rubrics1 = rubricDbSet.ToDictionary(item => item.Id, item => item);
            Dictionary<int, RT.Domain.Models.Region> regions1 = regionDbSet.ToDictionary(item => item.Id, item => item);

            foreach (RT.Domain.Models.Rubric pop in rubricDbSet)
            {
                var list = new StackListQueue<string>(pop.Id.ToString());
                short level = 1;
                for (RT.Domain.Models.Rubric current = pop;
                    (current.ParentId ?? 0) != 0;
                    current = rubrics1[current.ParentId ?? 0])
                {
                    list.Add(current.ParentId.ToString());
                    level++;
                }
                pop.IdPath = string.Join(".", list.GetReverse());
                pop.Level = level;
                pop.HasChild = pop.Children.Any();
                Modified.Add(new Rubric(pop));
            }

            foreach (RT.Domain.Models.Region pop in regionDbSet)
            {
                var list = new StackListQueue<string>(pop.Id.ToString());
                short level = 1;
                for (RT.Domain.Models.Region current = pop;
                    (current.ParentId ?? 0) != 0;
                    current = regions1[current.ParentId ?? 0])
                {
                    list.Add(current.ParentId.ToString());
                    level++;
                }
                pop.IdPath = string.Join(".", list.GetReverse());
                pop.Level = level;
                pop.HasChild = pop.Children.Any();
                Modified.Add(new Region(pop));
            }

            SaveChanges();
        }

        private void Refresh()
        {
            actionDbSet.Clear();
            rubricDbSet.Clear();
            regionDbSet.Clear();
            costDbSet.Clear();

            actionRootSet.Clear();
            rubricRootSet.Clear();
            regionRootSet.Clear();

            Dictionary<int, RT.Domain.Models.Action> actions1 = new Dictionary<int, RT.Domain.Models.Action>();
            Dictionary<int, RT.Domain.Models.Rubric> rubrics1 = new Dictionary<int, RT.Domain.Models.Rubric>();
            Dictionary<int, RT.Domain.Models.Region> regions1 = new Dictionary<int, RT.Domain.Models.Region>();
            foreach (
                var item in AdvertManager.Database.Persistent.Load(new Action()).Select(r => new Action(r).ToObject()))
            {
                actions1.Add(item.Id, item);
                actionDbSet.Add(item);
            }
            foreach (
                var item in AdvertManager.Database.Persistent.Load(new Rubric_Cost()).Select(r => new Rubric_Cost(r).ToObject()))
            {
                costDbSet.Add(item);
            }
            for (int i = 0; i < 10; i++)
            {
                foreach (
                    var item in
                        AdvertManager.Database.Persistent.Load(new Rubric { Level = i })
                            .Select(r => new Rubric(r).ToObject()))
                {
                    if ((item.ParentId ?? 0) != 0 && rubrics1.ContainsKey(item.ParentId ?? 0))
                        item.IdPath = rubrics1[item.ParentId ?? 0].IdPath + "." + item.Id;
                    else item.IdPath = item.Id.ToString();
                    rubrics1.Add(item.Id, item);
                    rubricDbSet.Add(item);
                }
                foreach (
                    var item in
                        AdvertManager.Database.Persistent.Load(new Region { Level = i })
                            .Select(r => new Region(r).ToObject()))
                {
                    if ((item.ParentId ?? 0) != 0 && regions1.ContainsKey(item.ParentId ?? 0))
                        item.IdPath = regions1[item.ParentId ?? 0].IdPath + "." + item.Id;
                    else item.IdPath = item.Id.ToString();
                    regions1.Add(item.Id, item);
                    regionDbSet.Add(item);
                }
            }

            foreach (RT.Domain.Models.Action action in actionDbSet) action.Children.Clear();
            foreach (RT.Domain.Models.Rubric rubric in rubricDbSet) rubric.Children.Clear();
            foreach (RT.Domain.Models.Region region in regionDbSet) region.Children.Clear();
            foreach (RT.Domain.Models.Rubric rubric in rubricDbSet) rubric.Costs.Clear();

            foreach (RT.Domain.Models.Action action in actionDbSet) actionRootSet.Add(action);
            foreach (RT.Domain.Models.Rubric rubric in rubricDbSet) if (rubric.Level < 4) rubricRootSet.Add(rubric);
            foreach (RT.Domain.Models.Region region in regionDbSet) if (region.Level < 4) regionRootSet.Add(region);

            foreach (RT.Domain.Models.Action action in actionDbSet) action.ActionDbSet = actionDbSet;
            foreach (RT.Domain.Models.Rubric rubric in rubricDbSet) rubric.RubricDbSet = rubricDbSet;
            foreach (RT.Domain.Models.Region region in regionDbSet) region.RegionDbSet = regionDbSet;
            foreach (RT.Domain.Models.Action action in actionDbSet) action.ActionRootSet = actionRootSet;
            foreach (RT.Domain.Models.Rubric rubric in rubricDbSet) rubric.RubricRootSet = rubricRootSet;
            foreach (RT.Domain.Models.Region region in regionDbSet) region.RegionRootSet = regionRootSet;
            foreach (RubricCost cost in costDbSet) cost.RubricDbSet = rubricDbSet;

            foreach (RT.Domain.Models.Rubric rubric in rubricDbSet)
                if ((rubric.ParentId ?? 0) != 0 && rubrics1.ContainsKey(rubric.ParentId ?? 0))
                    rubrics1[rubric.ParentId ?? 0].Children.Add(rubric);
            foreach (RT.Domain.Models.Region region in regionDbSet)
                if ((region.ParentId ?? 0) != 0 && regions1.ContainsKey(region.ParentId ?? 0))
                    regions1[region.ParentId ?? 0].Children.Add(region);
            foreach (RubricCost cost in costDbSet) rubrics1[cost.RubricId].Costs.Add(cost);
        }

        public static FakeDbContext Create()
        {
            return new FakeDbContext();
        }
    }

    public class NpgsqlDbContext : IdentityDbContext
    {
        public NpgsqlDbContext()
            : base("NpgsqlConnection")
        {
        }

        public DbSet<Action> Actions { get; set; }

        public DbSet<RT.Domain.Models.Rubric> Rubrics { get; set; }

        public DbSet<RT.Domain.Models.Region> Regions { get; set; }

        public static NpgsqlDbContext Create()
        {
            return new NpgsqlDbContext();
        }
    }

    public class DevartDbContext : IdentityDbContext
    {
        public DevartDbContext()
            : base("DevartConnection")
        {
        }

        public DbSet<Action> Actions { get; set; }

        public DbSet<RT.Domain.Models.Rubric> Rubrics { get; set; }

        public DbSet<RT.Domain.Models.Region> Regions { get; set; }

        public static DevartDbContext Create()
        {
            return new DevartDbContext();
        }
    }
}