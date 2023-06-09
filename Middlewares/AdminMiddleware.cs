﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using CrmAssistant.Models.Enums;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace CrmAssistant.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AdminMiddleware : IMiddleware
    {

        public AdminMiddleware()
        {
            
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<AdminMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var role = context.User.FindFirstValue(ClaimTypes.Role);

            if (role != Role.ADMINISTRATOR.ToString())
            {
                context.Response.Redirect("/Home");
            }

            await next(context);
        }
    }

}
