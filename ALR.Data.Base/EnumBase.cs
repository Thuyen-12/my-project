namespace ALR.Data.Base
{
    public class EnumBase
    {
        public enum STATUS
        {
            Active,
            NonActive,
            PendingActive
        }

        public enum GENDER
        {
            Male,
            Female,
            Other
        }

        public enum ROLE
        {
            Administrator,
            Landlord,
            Tenant,
            Guest
        }

        public enum RequirementStatus
        {
            Pending,
            Accept,
            Cancel
        }

        public enum ACTIONMODIFIED
        {
            Delete,
            Update
        }

        public enum AlrResult
        {
            Success,
            Failed,
            NullObject
        }
    }
}