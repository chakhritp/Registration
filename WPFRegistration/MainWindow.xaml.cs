using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net.Http;
using System.Net.Http.Headers;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Shared;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Builders;

namespace WPFRegistration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class Member
        {
            [BsonId]
            public MongoDB.Bson.BsonObjectId Id { get; set; }
            public int MemberId { get; set; }
            public string Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Sex { get; set; }
            public int Age { get; set; }
            public string Address { get; set; }
        }

        MongoClient mongo = new MongoClient("mongodb://localhost");
        MongoServer server;
        MongoDatabase database;
        MongoCollection<Member> _members;
        Member _member;

        public MainWindow()
        {
            InitializeComponent();

            connectDB();
            resetControls();
            bindGrid();
        }

        private void connectDB()
        {
            server = mongo.GetServer();
            server.Connect();
            database = server.GetDatabase("RegistrationDB");
        }

        private void resetControls()
        {
            cmbTitle.SelectedIndex = 0;
            txtFName.Clear();
            txtLName.Clear();
            cmbSex.SelectedIndex = 0;
            txtAge.Clear();
            txtAddr.Clear();
        }

        private void bindGrid()
        {
            // bind the existing member collection<table> record in grid
            _members = database.GetCollection<Member>("Members");

            dtgMembers.DataContext = _members.FindAll();
            dtgMembers.ItemsSource = _members.FindAll();
        }

        private void reversBind(Member _member)
        {
            cmbTitle.Text = _member.Title;
            txtFName.Text = _member.FirstName;
            txtLName.Text = _member.LastName;
            cmbSex.Text = _member.Sex;
            txtAge.Text = _member.Age.ToString();
            txtAddr.Text = _member.Address;
        }

        private Member getMember(int id)
        {
            IMongoQuery query = Query.EQ("MemberId", id);

            return _members.Find(query).FirstOrDefault();
        }
        
        private void createMember(Member _member)
        {
            _member.Id = ObjectId.GenerateNewId();

            _members.Insert(_member);
        }

        private void updateMember(Member _member)
        {
            IMongoQuery query = Query.EQ("MemberId", _member.MemberId);
            IMongoUpdate update = Update
                .Set("Title", _member.Title)
                .Set("FirstName", _member.FirstName)
                .Set("LastName", _member.LastName)
                .Set("Sex", _member.Sex)
                .Set("Age", _member.Age)
                .Set("Address", _member.Address);
            SafeModeResult result = _members.Update(query, update);
        }

        private void deleteUser()
        {
            IMongoQuery query = Query.EQ("MemberId", _member.MemberId);
            SafeModeResult result = _members.Remove(query);
        }

        private void dtgMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Member val = (Member)e.AddedItems[0];
                _member = getMember(val.MemberId);

                reversBind(_member);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            resetControls();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            // insert the record in to the mydb info collaction<table> 
            _member = new Member { MemberId = (int)database.GetCollection("Members").Count() + 1
                                    , Title = cmbTitle.Text
                                    , FirstName = txtFName.Text
                                    , LastName = txtLName.Text
                                    , Sex = cmbSex.Text
                                    , Age = Convert.ToInt16(txtAge.Text)
                                    , Address = txtAddr.Text };

            createMember(_member);

            resetControls();
            bindGrid();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            dtgMembers.Focusable = false;

            _member.Title = cmbTitle.Text;
            _member.FirstName = txtFName.Text;
            _member.LastName = txtLName.Text;
            _member.Sex = cmbSex.Text;
            _member.Age = Convert.ToInt16(txtAge.Text);
            _member.Address = txtAddr.Text;

            updateMember(_member);

            resetControls();
            bindGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            deleteUser();

            resetControls();
            bindGrid();
        }

    }
}
