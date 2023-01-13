using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.MVVM.Models;

namespace Tasker.MVVM.ViewModels
{
     [AddINotifyPropertyChangedInterface]
     public class MainViewModel
     {
          public ObservableCollection<Category> Categories { get; set; }
          public ObservableCollection<MyTask> Tasks { get; set; }

          public MainViewModel()
          {
               FillData();
               Tasks.CollectionChanged += Tasks_CollectionChanged;
          }

          private void Tasks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
          {
               UpdateData();
          }

          private void FillData()
          {
               Categories = new ObservableCollection<Category>
               {
                    new Category
                    {
                         Id = 1,
                         CategoryName = "Proiect",
                         Color = "#CF14DF"
                    },
                    new Category
                    {
                         Id = 2,
                         CategoryName = "Testare",
                         Color = "#df6f14"
                    },
                    new Category
                    {
                         Id = 3,
                         CategoryName = "Productie",
                         Color = "#14df80"
                    }
               };

               Tasks = new ObservableCollection<MyTask>
               {
                    new MyTask
                    {
                         TaskName = "Aplicatie .NET Maui",
                         Completed = false,
                         CategoryId = 1
                    },
                    new MyTask
                    {
                         TaskName = "Testarea aplicatiei",
                         Completed = false,
                         CategoryId = 1
                    },
                    new MyTask
                    {
                         TaskName = "Livrarea in Productie",
                         Completed = false,
                         CategoryId = 2
                    },
                    new MyTask
                    {
                         TaskName = "Feedback Client",
                         Completed = false,
                         CategoryId = 2
                    },
                    new MyTask
                    {
                         TaskName = "Support & Mentenanta",
                         Completed = true,
                         CategoryId = 2
                    },
                    new MyTask
                    {
                         TaskName = "Client fericit",
                         Completed = false,
                         CategoryId = 3
                    },
                    new MyTask
                    {
                         TaskName = "Aplicatie moderna",
                         Completed = false,
                         CategoryId = 3
                    },
               };

               UpdateData();
          }

          public void UpdateData()
          {
               foreach (var c in Categories)
               {
                    var tasks = from t in Tasks
                                where t.CategoryId == c.Id
                                select t;

                    var completed = from t in tasks
                                    where t.Completed == true
                                    select t;

                    var notCompleted = from t in tasks
                                       where t.Completed == false
                                       select t;



                    c.PendingTasks = notCompleted.Count();
                    c.Percentage = (float)completed.Count() / (float)tasks.Count();
               }
               foreach (var t in Tasks)
               {
                    var catColor =
                         (from c in Categories
                          where c.Id == t.CategoryId
                          select c.Color).FirstOrDefault();
                    t.TaskColor = catColor;
               }
          }

     }
}
