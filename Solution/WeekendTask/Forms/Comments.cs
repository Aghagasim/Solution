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
    public partial class Comments : Form
    {
        private readonly CRUDContext _context;
        RestClient client;
        List<Post> posts;
        List<User> users;
        List<Comment> comments;
        int selectedId;


        public Comments()
        {
            InitializeComponent();
            _context = new CRUDContext();
            client = new RestClient("https://jsonplaceholder.typicode.com");

            posts = new List<Post>();
            users = new List<User>();
            comments = new List<Comment>();
        }

        public void FillGridView()
        {
            foreach (Comment item in comments)
            {
                int selectedUserId = _context.Posts.Find(item.PostId).UserId;
                User user = _context.Users.FirstOrDefault(u => u.Id == selectedUserId);

                dgvComments.Rows.Add(
                    item.Id,
                    item.Name,
                    item.Email,
                    item.Body,
                    user.Name
                    );
            }
        }
        public void GetComments()
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

        private void Comments_Load(object sender, EventArgs e)
        {
            GetComments();
            FillGridView();
        }
    }
}
