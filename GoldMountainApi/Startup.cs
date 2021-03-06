﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GoldMountainApi.Controllers.Helper;
using GoldMountainApi.Models;
using GoldMountainApi.Services;
using GoldMountainShared.Dto.Bank;
using GoldMountainShared.Dto.Credit;
using GoldMountainShared.Dto.Insur;
using GoldMountainShared.Dto.Shared;
using GoldMountainShared.Storage;
using GoldMountainShared.Storage.Documents;
using GoldMountainShared.Storage.Interfaces;
using GoldMountainShared.Storage.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GoldMountainApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                //options.RequireHttpsMetadata = false;
                options.Authority = Configuration["Authentication:Authority"];
                options.Audience = Configuration["Authentication:Audience"];

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["Authentication:Authority"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:ClientSecret"]))
                };
            });

            services.Configure<DbSettings>(options =>
            {
                options.ConnectionString = Configuration["MongoConnection:ConnectionString"];
                options.Database = Configuration["MongoConnection:Database"];
            });

            services.AddCors(o => o.AddPolicy("DevPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddCors(o => o.AddPolicy("ProdPolicy", builder =>
            {
                builder.SetIsOriginAllowed(origin => origin.Equals(
                        Configuration.GetSection("Host:Name").Value))
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages",
            //        Configuration["Authentication:Authority"])));
            //});
            //services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBankAccountRepository, BankAccountRepository>();
            services.AddTransient<ICreditCardRepository, CreditCardRepository>();
            services.AddTransient<IInsurAccountRepository, InsurAccountRepository>();
            services.AddTransient<ILifeInsurAccountRepository, LifeInsurAccountRepository>();
            services.AddTransient<IEfundAccountRepository, EfundAccountRepository>();
            services.AddTransient<IPensionAccountRepository, PensionAccountRepository>();
            services.AddTransient<IMortgageInsurAccountRepository, MortgageInsurAccountRepository>();
            services.AddTransient<IProviderRepository, ProviderRepository>();
            services.AddTransient<IInstitutionRepository, InstitutionRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IDataService, DataService>();
            services.AddSingleton<IValidationHelper, ValidationHelper>();
            services.AddSingleton<IEmailHelper, EmailHelper>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("DevPolicy");
                app.UseAuthentication();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Ooops... something wrong happened");
                    });
                });

                app.UseCors("ProdPolicy");
                app.UseAuthentication();
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<string, Guid>().ConvertUsing(s => Guid.Parse(s));
                cfg.CreateMap<string, Guid?>().ConvertUsing(s => String.IsNullOrWhiteSpace(s) ? (Guid?)null : Guid.Parse(s));
                cfg.CreateMap<Guid?, string>().ConvertUsing(g => g?.ToString("N"));
                cfg.CreateMap<Guid, string>().ConvertUsing(g => g.ToString("N"));

                cfg.CreateMap<BankAccountCreatingDto, BankAccountDoc>()
                    .ForMember(dest => dest.Id, _ => Guid.NewGuid());

                cfg.CreateMap<TransactionDoc, TransactionDto>();
                cfg.CreateMap<TransactionDto, TransactionDoc>();

                cfg.CreateMap<MortgageDoc, MortgageDto>();

                cfg.CreateMap<BankAccountDoc, BankAccountDto>();
                cfg.CreateMap<CreditCardDoc, CreditCardDto>();
                cfg.CreateMap<BankAccountDto, BankAccountDoc>();
                cfg.CreateMap<CreditCardDto, CreditCardDoc>();

                cfg.CreateMap<ContactMessageDto, ContactMessageDoc>();

                cfg.CreateMap<SeInsurAccountDoc, SeInsurAccountDto>();
                cfg.CreateMap<ProvidentFundAccountDoc, ProvidentFundAccountDto>();
                cfg.CreateMap<StudyFundAccount, StudyFundAccountDto>();
                cfg.CreateMap<PensionFundAccountDoc, PensionFundAccountDto>();
                cfg.CreateMap<MortgageInsurAccountDoc, MortgageInsurAccountDto>();
            });

            app.UseMvc();
        }
    }
}
