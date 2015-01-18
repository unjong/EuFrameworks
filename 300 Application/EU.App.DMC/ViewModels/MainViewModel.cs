using EU.Wpf.Mvvm;
using EU.Wpf.Mvvm.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace EU.App.DMC.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        enum emCommands
        {
            New, Open, Close, Save, SaveAs, Snapin
        }

        public override void InitViewModel()
        {
            base.InitViewModel();

            this.StatusMessage = "Ready";

            InitMainMenu();
        }

        private void InitMainMenu()
        {
            var menuList = new List<IMenuModel>();
            {
                var root = MenuModel.Create("_FILE");
                root.Items.Add(MenuModel.Create("_New Workspace", this.CreateCommand(emCommands.New)));
                root.Items.Add(MenuModel.Create("_Open", this.CreateCommand(emCommands.Open)));
                root.Items.Add(MenuModel.Create("_Close", this.CreateCommand(emCommands.Close)));
                root.Items.Add(MenuModel.CreateSeparator());
                root.Items.Add(MenuModel.Create("_Save", this.CreateCommand(emCommands.Save), "Ctrl+S"));
                root.Items.Add(MenuModel.Create("Save _As...", this.CreateCommand(emCommands.SaveAs)));
                root.Items.Add(MenuModel.CreateSeparator());
                root.Items.Add(MenuModel.Create("Snapin Add/Remove", this.CreateCommand(emCommands.Snapin)));
                root.Items.Add(MenuModel.CreateSeparator());
                root.Items.Add(MenuModel.Create("E_xit", Command.Create((s, p) => { Application.Current.Shutdown(); }), "Alt+F4"));
                menuList.Add(root);
            }
            //{
            //    var root = MenuModel.Create("_WINDOW");
            //    root.Items.Add(MenuModel.Create("_Float"));
            //    root.Items.Add(MenuModel.Create("Doc_K"));
            //    menuList.Add(root);
            //}
            {
                var root = MenuModel.Create("_HELP");
                root.Items.Add(MenuModel.Create("View _Help", null, "Ctrl+F1"));
                root.Items.Add(MenuModel.CreateSeparator());
                root.Items.Add(MenuModel.Create("_About"));
                menuList.Add(root);
            }

            MainMenu = menuList;
        }

        public IEnumerable<IMenuModel> MainMenu { get; set; }

        public string StatusMessage { get { return _StatusMessage; } set { _StatusMessage = value; RaisePropertyChanged(); } }
        private string _StatusMessage;

        public WorkspaceViewModel Workspace { get { return _Workspace; } set { _Workspace = value; RaisePropertyChanged(); } }
        private WorkspaceViewModel _Workspace;

        public override void CommandOnExcute(object key, object parameter)
        {
            if (key.Equals(emCommands.New))
            {
                this.Workspace = Services.ViewModelLocator.Current.GetInstance<WorkspaceViewModel>();

                //var path = SelectConsolePath();
                //if (path == null) return;
            }
            else if (key.Equals(emCommands.Open))
            {
                //this.Workspace.Open();
            }
            else if (key.Equals(emCommands.Close))
            {
                this.Workspace.Close();
                this.Workspace = null;
            }
            else if (key.Equals(emCommands.Save))
            {
                //this.Workspace.Save();
            }
            else if (key.Equals(emCommands.SaveAs))
            {
                //this.Workspace.SaveAs();
            }
            else if (key.Equals(emCommands.Snapin))
            {
                this.Workspace.LoadSnapin();
            }
        }

        public override bool CommandOnCanExcute(object key, object parameter)
        {
            if (key.Equals(emCommands.New))
                return this.Workspace == null;

            else if (key.Equals(emCommands.Open))
                return this.Workspace == null;

            else if (key.Equals(emCommands.Close))
                return this.Workspace != null;

            else if (key.Equals(emCommands.Save))
                return this.Workspace != null;

            else if (key.Equals(emCommands.SaveAs))
                return this.Workspace != null;

            else if (key.Equals(emCommands.Snapin))
                return this.Workspace != null;

            return true;
        }

        private string SelectConsolePath()
        {
            var consolePath = System.IO.Path.GetFullPath(@"Consoles");
            var directories = System.IO.Directory.GetDirectories(consolePath);

            var r = EU.Wpf.Windows.WindowService.SelectItemDialog(directories);
            if (r.IsSuccess != true) return null;

            var path = r.Parameters.ElementAt(0);
            return path;
        }
    }
}

