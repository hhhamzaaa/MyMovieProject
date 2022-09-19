using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static MoviesDatabase.Pages.MoviesModel;

namespace MoviesDatabase.Pages
{
    public class AddModel : PageModel
    {
        public MovieInfo movieInfo = new MovieInfo();
        public String ErrorMessage = "";
        public String SuccessMessage = "";
        public void OnGet()
        {
        }

        public void onPost()
        {
            movieInfo.MovieName = Request.Form["MovieName"];
            movieInfo.MovieName = Request.Form["MovieRelease"];
            movieInfo.MovieName = Request.Form["RoomId"];

            if(movieInfo.MovieName.Length == 0 || movieInfo.MovieRelease.Length == 0 || movieInfo.RoomId.Length == 0)
            {
                ErrorMessage = "all fields all required";
                return;
            }

            //save the Movie Info to the database
            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\benab\\OneDrive\\Documents\\MoviesDatabase.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSER INTO Movies" +
                                 "(MovieName, MovieRelease, RoomId" +
                                 "@MovieName, @MovieRelease, @RoomId";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MovieName", movieInfo.MovieName);
                        command.Parameters.AddWithValue("@MovieRelease", movieInfo.MovieRelease);
                        command.Parameters.AddWithValue("@RoomId", movieInfo.RoomId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorMessage=ex.Message;
                return;
            }

            movieInfo.MovieName = ""; movieInfo.MovieRelease = ""; movieInfo.RoomId = "";
            SuccessMessage = "new cliend added successfully";

            Response.Redirect("Movies");
        }
    }
}
