using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeekendTask.DAL;
using WeekendTask.Models;

namespace WeekendTask.Forms
{
    public partial class UsersForm : Form
    {
        private readonly CRUDContext _context;
        RestClient client;

        List<Post> posts;
        List<User> users;

        public UsersForm()
        {
            InitializeComponent();
            _context = new CRUDContext();
            client = new RestClient("https://jsonplaceholder.typicode.com");

            users = new List<User>();
            posts = new List<Post>();
        }

        public void FillGridView()
        {
            foreach (User user in users)
            {
                dataGridView1.Rows.Add(
                    user.Id,
                    user.Name,
                    user.Username,
                    user.Email,
                    user.Website,
                    user.Posts.Count()
                    );
            }
        }

        public void GetUsers()
        {
            var request = new RestRequest("users");

            IRestResponse response = client.Execute(request);

            users = JsonConvert.DeserializeObject<List<User>>(response.Content);


            var requestPost = new RestRequest("posts");

            IRestResponse responsePost = client.Execute(requestPost);

            posts = JsonConvert.DeserializeObject<List<Post>>(responsePost.Content);

            _context.Users.AddRange(users);
            _context.Posts.AddRange(posts);
            _context.SaveChanges();
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            GetUsers();
            FillGridView();
        }
    }
}
