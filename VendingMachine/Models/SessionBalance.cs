using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using VendingMachine.Infrastructure;

namespace VendingMachine.Models {

    public class SessionBalance : Balance {

        public static Balance GetBalance(IServiceProvider services) {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionBalance userBalance = session?.GetJson<SessionBalance>("UserBalance") ?? new SessionBalance();
            userBalance.Session = session;
            return userBalance;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void Add(decimal value) {
            base.Add(value);
            Session.SetJson("UserBalance", this);
        }

        public override void Remove(decimal value) {
            base.Remove(value);
            Session.SetJson("UserBalance", this);
        }

        public override void Clear() {
            base.Clear();
            Session.SetJson("UserBalance", this);
        }
    }
}
