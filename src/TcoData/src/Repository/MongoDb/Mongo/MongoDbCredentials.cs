namespace TcOpen.Inxton.Data.MongoDb
{
    /// <summary>
    /// To use MongoDbCrendetials you must at first create a database with users
    /// 1. Start and c onnect to mongo database
    ///
    /// <code>
    ///     "C:\Program Files\MongoDB\Server\4.4\bin\mongod.exe" --dbpath C:\DATA\DB6\ --bind_ip_all
    /// </code>
    ///
    /// <code>
    ///     "C:\Program Files\MongoDB\Server\4.4\bin\mongo.exe" --host <HOSTNAME> --port <PORT>
    /// </code>
    ///
    /// 2.Switch to the admin database.
    ///
    /// <code>
    ///     use admin
    /// </code>
    ///
    /// 3. Create user in the admin database
    /// <code>
    ///     db.createUser(
    ///       {
    ///         user: "TcOpenAdmin",
    ///         pwd: "changeMeToAStrongPassword",
    ///         roles: [ "root" ]
    ///       }
    ///     )
    /// </code>
    ///
    /// 4. Verify that the user has been created
    /// <code>
    ///     show users
    /// </code>
    ///
    /// You should get something like this
    /// <code>
    ///     {
    ///         "_id" : "admin.TcOpenAdmin",
    ///         "userId" : UUID("78617a69-1b33-407d-97a8-4efe7cbaacf8"),
    ///         "user" : "TcOpenAdmin",
    ///         "db" : "admin",
    ///         "roles" : [
    ///                 {
    ///                         "role" : "root",
    ///                         "db" : "admin"
    ///                 }
    ///         ],
    ///         "mechanisms" : [
    ///                 "SCRAM-SHA-1",
    ///                 "SCRAM-SHA-256"
    ///         ]
    /// }
    /// </code>
    ///
    /// 5. Exit the mongo shell
    /// <code>
    ///     db.shutdownServer()
    ///     exit
    /// </code>
    ///
    /// 6. Start the database with --auth flag
    /// <code>
    ///     "C:\Program Files\MongoDB\Server\4.4\bin\mongod.exe"  --dbpath C:\DATA\DB6\ --bind_ip_all --auth
    /// </code>
    /// </summary>
    public class MongoDbCredentials
    {
        public MongoDbCredentials(string usersDatabase, string user, string password)
        {
            UsersDatabase = usersDatabase;
            Username = user;
            Password = password;
        }

        public string UsersDatabase { get; }
        public string Username { get; }
        public string Password { get; }
    }
}
