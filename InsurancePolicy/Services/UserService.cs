using InsurancePolicy.Exceptions.RoleException;
using InsurancePolicy.Exceptions.UserExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class UserService:IUserService
    {
        private readonly IRepository<User> _repository;
        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }
        public Guid Add(User user)
        {
            _repository.Add(user);
            return user.UserId;
        }

        public bool Delete(Guid id)
        {
            var user = _repository.GetById(id);
            if (user != null)
            {
                _repository.Delete(user);
                return true;
            }
            throw new UserNotFoundException("No such User found to delete");
        }

        public User GetById(Guid id)
        {
            var user = _repository.GetById(id);
            if (user != null)
                return user;
            throw new UserNotFoundException("No such User found");
        }

        public List<User> GetAll()
        {
            var users = _repository.GetAll().ToList();
            if (users.Count == 0)
                throw new UsersDoesNotExistException("No Users Exist");
            return users;
            
        }

        public bool Update(User User)
        {
            var existingUser = _repository.GetAll().AsNoTracking().FirstOrDefault(u=>u.UserId == User.UserId);
            if (existingUser != null)
            {
                _repository.Update(User);
                return true;
            }
            throw new UserNotFoundException("No such User found");
        }
    }
}
