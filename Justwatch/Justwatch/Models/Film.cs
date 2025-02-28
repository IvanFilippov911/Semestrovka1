

using Justwatch.Models.Sub_models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Justwatch.Models;

public class Film
{
    public int film_id { get; set; }

    public string name { get; set; }

    public string original_name { get; set; }

    public string release_date { get; set; }

    public string description { get; set; }

    public string country_of_production { get; set; }

    public string age_rating { get; set; }

    public string genre { get; set; }

    public string producer { get; set; }

    public string image_url { get; set; }

    public decimal rating { get; set; }

}


