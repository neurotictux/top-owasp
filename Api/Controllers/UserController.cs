using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Api.Auth;
using Shop.Api.Helpers;
using Shop.Api.Infra;
using Shop.Api.Infra.Models;
using Shop.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shop.Api.Controllers
{
  [AppAuthorize(UserType.Admin)]
  [Route("api/[controller]")]
  public class UserController : BaseController
  {
    public UserController(AppDbContext context) : base(context) { }

    [HttpGet]
    public IActionResult Get()
    {
      var users = context.User
      .Select(p => new UserViewModel(p)).ToList();
      return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var user = context.User.Find(id);

      if (user == null)
        return NotFound();

      return Ok(new UserViewModel(user));
    }

    [HttpPost]
    public IActionResult Post([FromBody]CreateUserViewModel model)
    {
      ValidateModel();

      User user = context.User.FirstOrDefault(p => p.Email == model.Email || p.Login == model.Login);

      if (user != null)
      {
        List<string> errors = new List<string>();
        if (user.Login == model.Login)
          errors.Add("'Login' already in use.");
        if (user.Email == model.Email)
          errors.Add("'Email' already in use.");
        throw new ValidateException() { Errors = errors };
      }

      context.User.Add(new User(model));
      context.SaveChanges();
      return Ok();
    }

    [HttpPut]
    public IActionResult Put([FromBody]UserViewModel model)
    {
      ValidateModel();

      User user = context.User.FirstOrDefault(p => p.Id != model.Id && (p.Email == model.Email || p.Login == model.Login));

      if (user != null)
      {
        List<string> errors = new List<string>();
        if (user.Login == model.Login)
          errors.Add("'Login' already in use.");
        if (user.Email == model.Email)
          errors.Add("'Email' already in use.");
        throw new ValidateException() { Errors = errors };
      }

      if (model.Type != UserType.Admin && !context.User.Any(p => p.Id != model.Id && p.Type == UserType.Admin))
        throw new ValidateException("The single administrator cannot have this privilege removed.");

      user = context.User.Find(model.Id);
      user.Email = model.Email;
      user.Login = model.Login;
      user.Type = model.Type;
      context.Entry(user).State = EntityState.Modified;
      context.SaveChanges();

      return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var user = context.User.FirstOrDefault(p => p.Id == id);
      if (user?.Type == UserType.Admin && !context.User.Any(p => p.Id != id && p.Type == UserType.Admin) )
        throw new ValidateException("The single administrator cannot be removed.");
      context.Remove(user);
      context.SaveChanges();
      return Ok();
    }
  }
}