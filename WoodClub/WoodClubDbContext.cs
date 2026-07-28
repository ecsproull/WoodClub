namespace WoodClub
{
    using CredentialManagement;
    using System;
    using System.Data.Entity.Core.EntityClient;
    using System.Data.SqlClient;

    public partial class WoodClubEntities
    {
        private static string GetConnectionString()
        {
            var cred = new Credential
            {
                Target = "WoodClubDB"
            };

            string userId = string.Empty;
            string password = string.Empty;
            if (cred.Load() && !string.IsNullOrEmpty(cred.Username) && !string.IsNullOrEmpty(cred.Password))
            {
                // Found in Credential Manager
                userId = cred.Username;
                password = cred.Password;
            }
            else
            {
                // Fallback to environment variables for services when running in session zero or when Credential Manager is not available.
                userId = Environment.GetEnvironmentVariable("SCW_DB_USER", EnvironmentVariableTarget.Machine);
                password = Environment.GetEnvironmentVariable("SCW_DB_PASSWORD", EnvironmentVariableTarget.Machine);

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
                {
                    throw new InvalidOperationException(
                        "Database credentials not found in Credential Manager or environment variables.");
                }
            }

            string serverName = Environment.GetEnvironmentVariable("SCW_DB_SERVER_NAME", EnvironmentVariableTarget.Machine);

            // Build the SQL connection string using SqlConnectionStringBuilder
            var sqlBuilder = new SqlConnectionStringBuilder
            {
                DataSource = serverName + @"\SQLEXPRESS",
                InitialCatalog = "WoodClub",
                UserID = userId,
                Password = password,
                Encrypt = false,
                TrustServerCertificate = true,
                MultipleActiveResultSets = true,
                ApplicationName = "EntityFramework"
            };

            // Build the Entity Framework connection string using EntityConnectionStringBuilder
            var entityBuilder = new EntityConnectionStringBuilder
            {
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = sqlBuilder.ToString(),
                Metadata = @"res://*/ModelWC.csdl|res://*/ModelWC.ssdl|res://*/ModelWC.msl"
            };

            return entityBuilder.ToString();
        }

        // Override the parameterless constructor to use our custom connection string
        public WoodClubEntities()
            : base(GetConnectionString())
        {
        }
    }
}
