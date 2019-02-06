using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
//using MahApps.Metro.Controls.Dialogs;

namespace GGGC.Admin
{
     internal interface IDialogService
    {
        //Task<MessageDialogResult> AskQuestionAsync(string title, string message);
        //Task<ProgressDialogController> ShowProgressAsync(string title, string message);
        //Task ShowMessageAsync(string title, string message);
    }

     internal class DialogService : IDialogService
     {
         private readonly MetroWindow metroWindow;

         public DialogService(MetroWindow metroWindow)
         {
             this.metroWindow = metroWindow;
         }

         //public Task<MessageDialogResult> AskQuestionAsync(string title, string message)
         //{
         //    var settings = new MetroDialogSettings()
         //    {
         //        AffirmativeButtonText = "Yes",
         //        NegativeButtonText = "No",
         //    };
         //    return metroWindow.ShowMessageAsync(title, message,
         //        MessageDialogStyle.AffirmativeAndNegative, settings);
         //}

         //public Task<ProgressDialogController> ShowProgressAsync(string title, string message)
         //{
         //    return metroWindow.ShowProgressAsync(title, message);
         //}

         //public Task ShowMessageAsync(string title, string message)
         //{
         //    return metroWindow.ShowMessageAsync(title, message);
         //}
     }
}
