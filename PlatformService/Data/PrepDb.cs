using PlatformService.Models;

namespace PlatformService.Data
{
   public static class PrepDb
   {
      public static void PrepPopulation(IApplicationBuilder app)
      {
         using (var serviceScope = app.ApplicationServices.CreateScope())
         {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
         }
      }
      private static void SeedData(AppDbContext context)
      {
         if (!context.Platforms.Any())
         {
            Console.WriteLine("[Seeding data] ========================");
            context.Platforms.AddRange(
               new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
               new Platform() { Name = "Sql server", Publisher = "Microsoft", Cost = "Payment" },
               new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing", Cost = "Free" }
            );
            context.SaveChanges();
            return;
         }
         Console.WriteLine("[Data existed] ========================");
      }
   }
}