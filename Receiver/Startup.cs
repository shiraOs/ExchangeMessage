using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Plain.RabbitMQ;
using RabbitMQ.Client;

namespace Receiver
{
    public class Startup
    {
        private const string QUEUE_NAME = "my_queue";
        private const string EXCHANGE_NAME = "receiver_exchange";
        private const string EXCHANGE_ROUTE = "receiver.*";
        private const string CONNECTION_URL = "amqp://guest:guest@localhost:5672";
        private const string SWAGGER_TITLE = "Receiver";
        private const string SWAGGER_VERSION = "v1";
        private const string SWAGGER_NAME = SWAGGER_TITLE + SWAGGER_VERSION;
        private const string SWAGGER_URL = "/swagger/v1/swagger.json";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SWAGGER_VERSION, new OpenApiInfo { Title = SWAGGER_TITLE, Version = SWAGGER_VERSION });
            });

            services.AddSingleton<IConnectionProvider>(new ConnectionProvider(CONNECTION_URL));
            services.AddSingleton<ISubscriber>(x => new Subscriber(x.GetService<IConnectionProvider>(),
                EXCHANGE_NAME,
                QUEUE_NAME,
                EXCHANGE_ROUTE,
                ExchangeType.Fanout));

            services.AddSingleton<IReceiverStorage, ReceiverStorage>();
            services.AddHostedService<ReceiverData>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(SWAGGER_URL, SWAGGER_NAME));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
