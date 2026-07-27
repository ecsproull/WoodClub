using CredsLib;
using System;
namespace WoodClub
{
    public partial class WoodClubEntities
    {
        private static string connectionString = String.Empty;
        private static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                // Retrieve the connection string from the configuration file
                connectionString = CredentialManager.GetConnectionString("wcrw", "WoodClubEntities");
            }
            return connectionString;
        }

        // Override the parameterless constructor to use our custom connection string
        // Be sure to delete the default constructor in the auto-generated code to avoid conflicts.
        public WoodClubEntities()
            : base(GetConnectionString())
        {
        }
    }
}
