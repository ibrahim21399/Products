namespace products.Infrastructure.Persistence
{
	public class EagleInitializer
	{
		public static void Initialize(ApplicationDbContext concreteContext)
		{
			var initializer = new EagleInitializer();
			initializer.SeedEverything(concreteContext);
		}
		public void SeedEverything(ApplicationDbContext context)
		{
			context.Database.EnsureCreated();
			SeedCountries(context);
		}
		private void SeedCountries(ApplicationDbContext context)
		{

		}
	}
}
