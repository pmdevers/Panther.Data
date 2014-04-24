Panther.Data
============

A Repository Pattern for PetaPoco

1. Getting Started.

Setting up the PetaPoco Repository for a WebApplication is quite easy. 
Just place the following lines in your Global.asax

	protected void Application_Start()
	{
		var connectionString = "Default";

		// Initializes the DbContextManager
		DbContextManager.Init(connectionString);
		// Initializes the Storage for the connections
		DbContextManager.InitStorage(new WebDbContextStorage(this));
	}

2. Creating a PetaPoco Entity

To use PetaPoco we need to create a POCO class see below an example class.

	[TableName("Pages")]
	[PrimaryKey("Id")]
	public class Page
	{
		public int Id { get; set; }
		public int TypeId { get; set; }
		public string Url { get; set; }

		[Ignore]
		public Page Parent { get; set; }
		[Ignore]
		public PageType PageType { get; set; }

		public int VersionNumber { get; set; }
	}

3. Creating a repository

For creating a repository we need 2 things an Interface describing the repository and a Class for the repository it self.
Here is an example repository for a Pages Table

	public interface IPageRepository : IRepository<Page>
	{
		Page GetPageByUrl(string url);
	}

Now we can create the repository that we can use in our code.

	public class PageRepository : GenericRepository<Page>, IPageRepository
	{
		public Page GetPageByUrl(string url)
		{
			return First("where url = @0", url);
		}
	}

4. Using the repository

To use the repository you can simply do the following.

	public void UseRepository()
	{
		//Initialize the repository
		var repository = new PageRepository();
		//Get the page by the url provided;
		var page = repository.GetPageByUrl("home");
	}


That is all what is needed to setup the repository.

I hope you will enjoy.