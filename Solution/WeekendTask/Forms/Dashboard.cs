using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeekendTask.DAL;
using WeekendTask.Forms;
using WeekendTask.Models;

namespace WeekendTask
{
    public partial class Dashboard : Form
    {
        private readonly CRUDContext _context;
        RestClient client;

        List<Post> posts;
        List<User> users;
        List<Comment> comments;
        public Dashboard()
        {
            InitializeComponent();
            _context = new CRUDContext();
            client = new RestClient("https://jsonplaceholder.typicode.com");

            users = new List<User>();
            posts = new List<Post>();
            comments = new List<Comment>();
        }

        public void GetUsers()
        {
            var request = new RestRequest("users");

            IRestResponse response = client.Execute(request);

            users = JsonConvert.DeserializeObject<List<User>>(response.Content);


            var requestPost = new RestRequest("posts");

            IRestResponse responsePost = client.Execute(requestPost);

            posts = JsonConvert.DeserializeObject<List<Post>>(responsePost.Content);

            var requestComments = new RestRequest("comments");

            IRestResponse responseComments = client.Execute(requestComments);

            comments = JsonConvert.DeserializeObject<List<Comment>>(responseComments.Content);

            _context.Users.AddRange(users);
            _context.Posts.AddRange(posts);
            _context.Comments.AddRange(comments);
            _context.SaveChanges();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            GetUsers();
            FillGridView();
        }

        public void FillGridView()
        {
            foreach (User user in users)
            {
                dgvDashboard.Rows.Add(
                    user.Id,
                    user.Name,
                    user.Username,
                    user.Email,
                    user.Posts.Count()
                    );
            }
        }

        private void PostsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PostsForm postsForm = new PostsForm();
            postsForm.ShowDialog();
        }

        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersForm usersForm = new UsersForm();
            usersForm.ShowDialog();
        }

        private void CommentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Comments comments = new Comments();
            comments.ShowDialog();
        }
    }
}
