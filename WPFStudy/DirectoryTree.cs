using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WPFStudy
{
    class DirectoryTree : INotifyPropertyChanged
    {
        public DirectoryTree()
        {
            this.isChecked = false;
        }

        public DirectoryTree(DirectoryTree parent, DirectoryInfo di)
            : this()
        {
            this.Parent = parent;
            this.Info = di;
        }

        public DirectoryTree(DirectoryTree parent, DirectoryInfo di, Boolean bChecked)
            : this(parent, di)
        {
            this.isChecked = bChecked;
        }

        public DirectoryInfo Info { get; set; }

        public DirectoryTree Parent { get; set; }

        public Boolean? IsChecked
        {
            get { return this.isChecked; }
            set
            {
                if (this.isChecked != value)
                {
                    this.isChecked = value;
                    NotifyPropertyChanged("IsChecked");

                    if (this.isChecked == true) // 如果节点被选中
                    {
                        if (this.dirs != null)
                            foreach (DirectoryTree dt in this.dirs)
                                dt.IsChecked = true;

                        if (this.Parent != null)
                        {
                            Boolean bExistUncheckedChildren = false;
                            foreach (DirectoryTree dt in this.Parent.Directories)
                                if (dt.IsChecked != true)
                                {
                                    bExistUncheckedChildren = true;
                                    break;
                                }
                            if (bExistUncheckedChildren)
                                this.Parent.IsChecked = null;
                            else
                                this.Parent.IsChecked = true;
                        }
                    }
                    else if (this.isChecked == false)   // 如果节点未选中
                    {
                        if (this.dirs != null)
                            foreach (DirectoryTree dt in this.dirs)
                                dt.IsChecked = false;

                        if (this.Parent != null)
                        {
                            Boolean bExistCheckedChildren = false;
                            foreach (DirectoryTree dt in this.Parent.Directories)
                                if (dt.IsChecked != false)
                                {
                                    bExistCheckedChildren = true;
                                    break;
                                }
                            if (bExistCheckedChildren)
                                this.Parent.IsChecked = null;
                            else
                                this.Parent.IsChecked = false;
                        }
                    }
                    else
                    {
                        if (this.Parent != null)
                            this.Parent.IsChecked = null;
                    }
                }
            }
        }

        public ObservableCollection<DirectoryTree> Directories
        {
            get
            {
                if (this.dirs == null)
                {
                    DirectoryInfo[] dis = Info.GetDirectories();
                    if (dis.Length > 0)
                    {
                        this.dirs = new ObservableCollection<DirectoryTree>();

                        foreach (DirectoryInfo di in dis)
                            this.dirs.Add(new DirectoryTree(this, di, this.isChecked == true));
                    }
                }
                return dirs;
            }
        }

        public static ObservableCollection<DirectoryTree> InitRoot()
        {
            ObservableCollection<DirectoryTree> dts = new ObservableCollection<DirectoryTree>();
            /*
                        DriveInfo[] drvs = DriveInfo.GetDrives();
                        foreach (DriveInfo drv in drvs)
                        {
                            if (drv.DriveType == DriveType.Fixed)
                            {
                                DirectoryTree dt = new DirectoryTree(null,new DirectoryInfo("C:\\Program Files"));
                                dts.Add(dt);
                            }
                        }
            */
            DirectoryTree dt = new DirectoryTree(null, new DirectoryInfo("C:\\Program Files"));
            dts.Add(dt);
            return dts;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private Boolean? isChecked;
        private ObservableCollection<DirectoryTree> dirs;
    }

}
