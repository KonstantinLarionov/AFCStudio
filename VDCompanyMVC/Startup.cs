using System;
using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Newtonsoft.Json;

namespace VDCompanyMVC
{
    internal static class Extensions
    {
        public static string ToJson( this object obj )
        {
            return JsonConvert.SerializeObject( obj );
        }
        public static T ToObject<T>( this string json )
        {
            return JsonConvert.DeserializeObject<T>( json );
        }
        public static string Get( this string str, string nameParam )
        {
            var arrayStr = str.Split( '-' );
            var selectSection = arrayStr.FirstOrDefault( x => x.Contains( nameParam ) );
            if ( !string.IsNullOrEmpty( selectSection ) )
            {
                return selectSection.Replace( nameParam, string.Empty ).Trim();
            }
            return string.Empty;
        }
        public static int ToInt( this string str )
        {
            try
            {
                return Convert.ToInt32( str );
            }
            catch
            {
                return 0;
            }
        }
        public static double ToDouble( this string str )
        {
            try
            {
                return Convert.ToDouble( str.Replace( ".", "," ) );
            }
            catch
            {
                return 0;
            }
        }
        public static string ToStringDF( this double number )
        {
            try
            {
                return Convert.ToString( number ).Replace( ",", "." );
            }
            catch
            {
                return "";
            }
        }
        public static string ToStringIF( this int number )
        {
            try
            {
                var countV = number.ToString().Count();
                switch ( countV )
                {
                    case 10:
                        return number.ToString( "# ### ### ###" );
                    case 9:
                        return number.ToString( "### ### ###" );
                    case 8:
                        return number.ToString( "## ### ###" );
                    case 7:
                        return number.ToString( "# ### ###" );
                    case 6:
                        return number.ToString( "### ###" );
                    case 5:
                        return number.ToString( "## ###" );
                    case 4:
                        return number.ToString( "# ###" );
                    default:
                        return number.ToString();
                }
            }
            catch
            {
                return "";
            }
        }
    }
    public class Startup
    {
        internal static Action<string> Logs = null;
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
            //Logs += Loger;
        }
        public void Loger( string message )
        {
            var main_path = Environment.CurrentDirectory + @"\Logs\" + DateTime.Now.Date.Day.ToString() + "." + DateTime.Now.Date.Month.ToString() + @"\";
            if ( Directory.Exists( main_path ) )
            {
                var file_path = $"{main_path}LOGAT{DateTime.Now.Hour.ToString()}-{DateTime.Now.Minute.ToString()}.txt";

                File.AppendAllText( file_path, message );
            }
            else
            {
                Directory.CreateDirectory( main_path );
                Loger( message );
            }
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices( IServiceCollection services )
        {
            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddDistributedMemoryCache();
            services.AddSession( options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds( 100000 );
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            } );
        }

        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler( "/Home/Error" );
                //app.UseHsts();
            }

            app.UseSession();
            app.UseStaticFiles();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints( endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}" );
            } );
        }
    }
}
