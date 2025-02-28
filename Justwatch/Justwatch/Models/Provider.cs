

using Justwatch.Models.Sub_models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Justwatch.Models;


public class Provider
{
    public int provider_id { get; set; }

    public string name { get; set; }

    public int duration { get; set; }

    public int price { get; set; }

    public List<ProviderData> film_providers { get; set; } = new();
}