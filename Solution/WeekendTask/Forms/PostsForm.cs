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
    public partial class PostsForm : Form
    {
        private readonly CRUDContext _context;
        RestClient client;
        List<Post> posts;
        List<User> users;

        public PostsForm()
        {
            InitializeComponent();
            _context = new CRUDContext();
            client = new RestClient("https://jsonplaceholder.typicode.com");

            posts = new List<Post>();
            users = new List<User>();
        }

        public void FillGridView()
        {
            foreach (Post post in posts)
            {
                dataGridView1.Rows.Add(
                    post.Id,
                    post.Title,
                    post.Body,
                    post.User.Name
                    );
            }
        }
        public void GetPosts()
        {
            var requestPost = new RestRequest("posts");

            IRestResponse responsePost = client.Execute(requestPost);

            posts = JsonConvert.DeserializeObject<List<Post>>(responsePost.Content);

            var requestUser = new RestRequest("users");

            IRestResponse responseUser = client.Execute(requestUser);

            users = JsonConvert.DeserializeObject<List<User>>(responseUser.Content);

            _context.Posts.AddRange(posts);
            _context.Users.AddRange(users);
            _context.SaveChanges();
        }

        private void PostsForm_Load(object sender, EventArgs e)
        {
            GetPosts();
            FillGridView();
        }
    }
}
