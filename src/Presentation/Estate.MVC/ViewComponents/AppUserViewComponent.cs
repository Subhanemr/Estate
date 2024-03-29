﻿using AutoMapper;
using Estate.Application.ViewModels;
using Estate.Domain.Entities;
using Estate.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Estate.MVC.ViewComponents
{
    public class AppUserViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _http;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AppUserViewComponent(IHttpContextAccessor http, UserManager<AppUser> userManager, IMapper mapper)
        {
            _http = http;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);
            if (user == null) throw new NotFoundException("Your request was not found");

            GetAppUserVM get = _mapper.Map<GetAppUserVM>(user);

            return View(get);
        }
    }
}
