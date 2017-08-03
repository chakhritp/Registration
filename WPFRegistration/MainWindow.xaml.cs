using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WPFRegistration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class Member
        {
            /*[BsonId]
            public MongoDB.Bson.BsonObjectId Id { get; set; }*/
            public int MemberId { get; set; }
            public string Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Sex { get; set; }
            public int Age { get; set; }
            public string Address { get; set; }
        }

        public class MongoDBContext
        {
            //public static bool IsSSL { get; set; }
            private IMongoDatabase _database { get; set; }

            public MongoDBContext()
            {
                try
                {
                    MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://localhost"));

                    //if (IsSSL)
                    //{
                    //    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                    //}

                    var mongoClient = new MongoClient(settings);

                    _database = mongoClient.GetDatabase("RegistrationDB");
                }
                catch (Exception ex)
                {
                    throw new Exception("Can not access to db server.", ex);
                }
            }

            public IMongoCollection<Member> Members
            {
                get
                {
                    return _database.GetCollection<Member>("Members");
                }
            }
        }

        HttpClient client = new HttpClient();
        HttpResponseMessage response;

        Member _member;

        MongoDBContext dbContext;

        public MainWindow()
        {
            InitializeComponent();

            //connectDB();

            client.BaseAddress = new Uri("http://localhost:49971");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            this.Loaded += MainWindow_Loaded;

            //resetControls();
            //bindGrid();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            response = await client.GetAsync("/api/Members");
            response.EnsureSuccessStatusCode(); // Throw on error code.
            resetControls();
            bindGrid();
        }

        private void connectDB()
        {
            dbContext = new MongoDBContext();
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

        private async void bindGrid()
        {
            // bind the existing member collection<table> record in grid
            //List<Member> memberList = dbContext.Members.Find(m => true).ToList();

            //dtgMembers.DataContext = memberList;
            //dtgMembers.ItemsSource = memberList;
            //dtgMembers.IsReadOnly = true;

            var members = await response.Content.ReadAsAsync<IEnumerable<Member>>();

            dtgMembers.DataContext = members;
            dtgMembers.ItemsSource = members;
            dtgMembers.IsReadOnly = true;
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
            return dbContext.Members.Find(m => m.MemberId == id).FirstOrDefault();
        }
        
        private void createMember(Member _member)
        {
            dbContext.Members.InsertOne(_member);
        }

        private void updateMember(Member _member)
        {
            dbContext.Members.ReplaceOne(m => m.MemberId == _member.MemberId, _member);
        }

        private void deleteMember(Member _member)
        {
            dbContext.Members.DeleteOne(m => m.MemberId == _member.MemberId);
        }

        private void dtgMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Member member = (Member)e.AddedItems[0];
                _member = getMember(member.MemberId);

                reversBind(_member);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            resetControls();
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            //insert the record in to the RegistrationDB members collaction<table> 
            /*_member = new Member { MemberId = (int)dbContext.Members.Count(new BsonDocument()) + 1
                                    , Title = cmbTitle.Text
                                    , FirstName = txtFName.Text
                                    , LastName = txtLName.Text
                                    , Sex = cmbSex.Text
                                    , Age = Convert.ToInt16(txtAge.Text)
                                    , Address = txtAddr.Text };*/

            //createMember(_member);
            try
            {
                var member = new Member()
                {
                    //name = txtStudentName.Text,
                    //id = int.Parse(txtStudentID.Text),
                    //gender = cbxGender.SelectedItem.ToString(),
                    //age = int.Parse(txtAge.Text)

                    /*MemberId = (int)dbContext.Members.Count(new BsonDocument()) + 1
                                    , */Title = cmbTitle.Text
                                    , FirstName = txtFName.Text
                                    , LastName = txtLName.Text
                                    , Sex = cmbSex.Text
                                    , Age = Convert.ToInt16(txtAge.Text)
                                    , Address = txtAddr.Text
                };
                var response = await client.PostAsJsonAsync("/api/Members/", member);
                response.EnsureSuccessStatusCode(); // Throw on error code.
                MessageBox.Show("Member Added Successfully", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                //studentsListView.ItemsSource = await GetAllStudents();
                //studentsListView.ScrollIntoView(studentsListView.ItemContainerGenerator.Items[studentsListView.Items.Count - 1]);
                resetControls();
                //bindGrid();
                dtgMembers.DataContext = await GetMembers(); ;
                dtgMembers.ItemsSource = await GetMembers(); ;
                dtgMembers.IsReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Student not Added, May be due to Duplicate ID");
            }

            //resetControls();
            //bindGrid();
        }

        public async Task<IEnumerable<Member>> GetMembers()
        {
            response = await client.GetAsync("/api/Members");
            response.EnsureSuccessStatusCode(); // Throw on error code.
            var members = await response.Content.ReadAsAsync<IEnumerable<Member>>();
            return members;
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
            deleteMember(_member);
            resetControls();
            bindGrid();
        }

    }
}
