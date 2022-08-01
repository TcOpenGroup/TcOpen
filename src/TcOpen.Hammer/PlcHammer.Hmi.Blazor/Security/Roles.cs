using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TcOpen.Inxton.Local.Security.Blazor;

using TcOpen.Inxton.Security;

namespace PlcHammer.Hmi.Blazor.Security
{
    internal class Roles
    {
        private static BlazorRoleGroupManager _roleGroupManager { get; set; }
        private static Roles _roles;

        private Roles()
        {
            _roleGroupManager.CreateRole(new Role(process_settings_access));
            _roleGroupManager.CreateRole(new Role(process_traceability_access));
            _roleGroupManager.CreateRole(new Role(can_user_open_technological_settings));
            _roleGroupManager.CreateRole(new Role(ground_position_start));
            _roleGroupManager.CreateRole(new Role(automat_start));
            _roleGroupManager.CreateRole(new Role(station_details));
            _roleGroupManager.CreateRole(new Role(technology_settings_access));
            _roleGroupManager.CreateRole(new Role(manual_start));
            _roleGroupManager.CreateRole(new Role(sequencer_step));
            _roleGroupManager.CreateRole(new Role(technology_automat_all));
            _roleGroupManager.CreateRole(new Role(technology_ground_all));
        }

        public const string process_settings_access = nameof(process_settings_access);
        public const string process_traceability_access = nameof(process_traceability_access);
        public const string can_user_open_technological_settings = nameof(can_user_open_technological_settings);
        public const string ground_position_start = nameof(ground_position_start);
        public const string automat_start = nameof(automat_start);
        public const string station_details = nameof(station_details);
        public const string technology_settings_access = nameof(technology_settings_access);
        public const string manual_start = nameof(manual_start);
        public const string sequencer_step = nameof(sequencer_step);
        public const string technology_automat_all = nameof(technology_automat_all);
        public const string technology_ground_all = nameof(technology_ground_all);

        public static void Create(BlazorRoleGroupManager roleGroupManager)
        {
            _roleGroupManager = roleGroupManager;
            _roles = new Roles();
        }

    }
}
