using APITest.Presistence.Contexts;

namespace APITest.Presistence.Repositories {
    public abstract class BaseRepositroy {
        protected readonly AppDbContext _context;

        public BaseRepositroy(AppDbContext context){
            _context = context;
        }
    }
}