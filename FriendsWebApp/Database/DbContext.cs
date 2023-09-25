using FriendsWebApp.Models;
using Microsoft.Data.SqlClient;

namespace FriendsWebApp.Database
{
    public class DbContext
    {

        private readonly SqlConnection connection;

        public DbContext(string connectionString)
        {
            //Constructor initializes connection
            this.connection = new SqlConnection(connectionString); //this? no in api
        }

        //Get all friends from database

        public List<Friend> GetAllFriends()
        {
            List<Friend> friends = new List<Friend>();
            using (connection)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                command.CommandText = "SELECT Id, FirstName, LastName, ImageName FROM Friends";
                
                //replace the about
                //string sql = "INSERT INTO [dbo].[Cupcakes] ([Name],[ImageName],[Description],[Price]) VALUES  (@Name, @ImageName, @Description,@Price)";
                //SqlCommand command = new SqlCommand(sql, connection); 

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                //Iterate through the result set
                while (reader.Read())
                {
                    Friend friend = new Friend();
                    friend.Id = reader.GetInt32(0);
                    friend.FirstName = reader.GetString(1);
                    friend.LastName = reader.GetString(2);
                    friend.ImageName = reader.GetString(3);
      

                    friends.Add(friend);
                } //close while loop

                //Close the connection
                reader.Close();
                connection.Close();
                return friends;

            }//close using connection

        }//close method

        public int AddNewFriend(Friend friend)
        {
            int rowAffected = 0;
            string sql = "INSERT INTO [dbo].[Friends] ([FirstName],[LastName],[ImageName]) VALUES (@FirstName, @LastName, @ImageName )";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@FirstName", friend.FirstName);
            command.Parameters.AddWithValue("@LastName", friend.LastName);
            command.Parameters.AddWithValue("@ImageName", friend.ImageName);


            connection.Open();
            rowAffected = command.ExecuteNonQuery();

            connection.Close();

            return rowAffected;

        }//close AddnewFriend

    }
}
