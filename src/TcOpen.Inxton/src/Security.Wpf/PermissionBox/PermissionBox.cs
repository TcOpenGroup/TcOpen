﻿namespace TcOpen.Inxton.Security.Wpf
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using TcOpen.Inxton.Abstractions.Security;
    using TcOpen.Inxton.Security;

    /// <summary>
    /// Provides permission control at the UI level.
    /// </summary>
    public class PermissionBox : ContentControl
    {
        /// <summary>
        /// Creates new instance of permission box.
        /// </summary>
        public PermissionBox()
        {            
            InstanceCount++;
            permissionBoxes.Add(this);

            if (InstanceCount == 1 && !DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                SecurityManager.Manager.Service.OnDeAuthenticating += UserAuthenticationChanged;
                SecurityManager.Manager.Service.OnUserAuthenticateFailed += UserAuthenticationChanged;
                SecurityManager.Manager.Service.OnUserAuthenticateSuccess += UserAuthenticationChanged;
                SecurityManager.Manager.Service.OnDeAuthenticated += UserAuthenticationChanged;
            }


            this.Loaded += PermissionBox_Loaded;           
        }

        private void PermissionBox_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateThis();           
        }

        private static IAuthenticationService AuthenticationService { get; }
        private static List<PermissionBox> permissionBoxes = new List<PermissionBox>();

        private static int InstanceCount;

        private static void UserAuthenticationChanged(string username) => Application.Current.Dispatcher.Invoke(UpdatePermissions);

        private static void UpdatePermissions()
        {
            foreach (var permissionBox in permissionBoxes)
            {
                permissionBox.UpdateThis();
            }
        }

        internal void UpdateThis()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                this.Visibility = Visibility.Visible;
                return;
            }

            if (string.IsNullOrEmpty(this.Permissions))
            {
                this.Visibility = Visibility.Hidden;
                return;
            }

            switch (this.SecurityMode)
            {
                case SecurityModeEnum.Invisible:
                    this.IsEnabled = HasPermission() ? true : false;
                    this.Visibility = HasPermission() ? Visibility.Visible : Visibility.Hidden;
                    break;
                case SecurityModeEnum.Disabled:
                    this.IsEnabled = HasPermission() ? true : false;
                    this.Visibility = Visibility.Visible;
                    break;
                //case SecurityModeEnum.Message:
                //    this.IsEnabled = false;
                //    this.Visibility = Visibility.Visible;
                //    this.Content = new InsufficientRightsBox() { IsEnabled = true };
                //    break;
                default:
                    break;
            }                     
        }
       
        private bool HasPermission()
        {
            return this.Permissions.Split('|')
                                   .Where(p => p != string.Empty)
                                   .Select(p => p.ToLower())
                                   .Intersect((SecurityManager.Manager.Principal.Identity as VortexIdentity).Roles.Select(role => role.ToLower()))
                                   .Any() ? true : false;
        }

        /// <summary>
        /// Gets or sets permission for this <see cref="PermissionBox"/>.        
        /// </summary>
        public string Permissions
        {
            get { return (string)GetValue(PermissionsProperty); }
            set { SetValue(PermissionsProperty, value); }
        }                

        /// <summary>
        /// Dependency property gets or sets <see cref="Permissions"/>
        /// </summary>
        public static readonly DependencyProperty PermissionsProperty =
            DependencyProperty.Register("Permissions", typeof(string), typeof(PermissionBox), new PropertyMetadata(""));


        /// <summary>
        /// Gets or sets the security mode <see cref="SecurityModeEnum"/>.
        /// </summary>
        public SecurityModeEnum SecurityMode
        {
            get { return (SecurityModeEnum)GetValue(SecurityModeProperty); }
            set { SetValue(SecurityModeProperty, value); }
        }

        /// <summary>
        /// Dependency property gets or sets <see cref="SecurityMode"/>.
        /// </summary>
        public static readonly DependencyProperty SecurityModeProperty =
            DependencyProperty.Register("SecurityMode", typeof(SecurityModeEnum), typeof(PermissionBox), new PropertyMetadata(SecurityModeEnum.Invisible));
    }
}