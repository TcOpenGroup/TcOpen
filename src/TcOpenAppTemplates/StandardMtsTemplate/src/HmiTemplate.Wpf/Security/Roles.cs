using TcOpen.Inxton.Local.Security;
using TcOpen.Inxton.Security;

namespace HmiTemplate.Wpf
{
    internal class Roles
    {
        public Roles()
        {
            SecurityManager.Manager.GetOrCreateRole(new Role(can_user_open_process_settings, "Administrator"));
            SecurityManager.Manager.GetOrCreateRole(new Role(can_user_open_process_traceability, "Administrator"));
            SecurityManager.Manager.GetOrCreateRole(new Role(controlled_unit_can_ground_position, "Operator"));
            SecurityManager.Manager.GetOrCreateRole(new Role(controlled_unit_can_automat, "Operator"));
            SecurityManager.Manager.GetOrCreateRole(new Role(controlled_unit_can_open_details, "Maintenance"));
            SecurityManager.Manager.GetOrCreateRole(new Role(can_user_open_technology_settings, "Maintenance"));
            SecurityManager.Manager.GetOrCreateRole(new Role(controlled_unit_can_manual, "Maintenance"));
            SecurityManager.Manager.GetOrCreateRole(new Role(sequencer_can_enter_step_mode, "Maintenance"));
            SecurityManager.Manager.GetOrCreateRole(new Role(sequencer_can_move_in_step_mode, "Maintenance"));
            SecurityManager.Manager.GetOrCreateRole(new Role(technology_can_start_ground_all, "Maintenance"));
            SecurityManager.Manager.GetOrCreateRole(new Role(technology_can_start_automat_all, "Maintenance"));
            SecurityManager.Manager.GetOrCreateRole(new Role(controlled_unit_can_open_components, "Maintenance"));

            SecurityManager.Manager.GetOrCreateRole(new Role(data_exchange_view_can_user_add_record, "Quality"));
            SecurityManager.Manager.GetOrCreateRole(new Role(data_exchange_view_can_user_delete_record, "Quality"));
            SecurityManager.Manager.GetOrCreateRole(new Role(data_exchange_view_can_user_load_from_plc, "Quality"));
            SecurityManager.Manager.GetOrCreateRole(new Role(data_exchange_view_can_user_send_to_plc, "Quality"));
            SecurityManager.Manager.GetOrCreateRole(new Role(data_exchange_view_can_update_record, "Quality"));

        }

        public const string can_user_open_process_settings = nameof(can_user_open_process_settings);
        public const string can_user_open_process_traceability = nameof(can_user_open_process_traceability);
        public const string can_user_open_technological_settings = nameof(can_user_open_technological_settings);
        public const string controlled_unit_can_ground_position = nameof(controlled_unit_can_ground_position);
        public const string controlled_unit_can_automat = nameof(controlled_unit_can_automat);
        public const string controlled_unit_can_open_details = nameof(controlled_unit_can_open_details);
        public const string can_user_open_technology_settings = nameof(can_user_open_technology_settings);
        public const string controlled_unit_can_manual = nameof(controlled_unit_can_manual);
        public const string sequencer_can_enter_step_mode = nameof(sequencer_can_enter_step_mode);
        public const string sequencer_can_move_in_step_mode = nameof(sequencer_can_move_in_step_mode);
        public const string technology_can_start_automat_all = nameof(technology_can_start_automat_all);
        public const string technology_can_start_ground_all = nameof(technology_can_start_ground_all);
        public const string controlled_unit_can_open_components = nameof(controlled_unit_can_open_components);

        public const string data_exchange_view_can_user_add_record = nameof(data_exchange_view_can_user_add_record);
        public const string data_exchange_view_can_user_delete_record = nameof(data_exchange_view_can_user_delete_record);
        public const string data_exchange_view_can_user_load_from_plc = nameof(data_exchange_view_can_user_load_from_plc);
        public const string data_exchange_view_can_user_send_to_plc = nameof(data_exchange_view_can_user_send_to_plc);
        public const string data_exchange_view_can_update_record = nameof(data_exchange_view_can_update_record);
    }
}
