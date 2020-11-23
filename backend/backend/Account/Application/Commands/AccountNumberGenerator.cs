using MediatR;
using MyBank.Account.Domain.Models;
using System.Runtime.Serialization;

namespace MyBank.Account.Application.Commands
{
    public static class AccountNumberGenerator
    {

        private static string[] availableAccountIds = { "NL81RABO8479662646", "NL96INGB7843842861", "NL77ABNA7852130259" };
        private static int index;

        public static string Next()
        {
            return availableAccountIds[index++];
        }

    }
}
