using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using EventProRegistration.Data;
using EventProRegistration.Services;
using Microsoft.OpenApi.Models;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using EventProRegistration.Middleware;

builder.Services.AddHealthChecks();
