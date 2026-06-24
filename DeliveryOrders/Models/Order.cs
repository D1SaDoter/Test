using System.ComponentModel.DataAnnotations;

namespace DeliveryOrders.Models;

public class Order
{
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    [Display(Name = "Номер заказа")]
    public string OrderNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите город отправителя")]
    [StringLength(100, ErrorMessage = "Город отправителя не должен превышать 100 символов")]
    [Display(Name = "Город отправителя")]
    public string SenderCity { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите адрес отправителя")]
    [StringLength(200, ErrorMessage = "Адрес отправителя не должен превышать 200 символов")]
    [Display(Name = "Адрес отправителя")]
    public string SenderAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите город получателя")]
    [StringLength(100, ErrorMessage = "Город получателя не должен превышать 100 символов")]
    [Display(Name = "Город получателя")]
    public string RecipientCity { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите адрес получателя")]
    [StringLength(200, ErrorMessage = "Адрес получателя не должен превышать 200 символов")]
    [Display(Name = "Адрес получателя")]
    public string RecipientAddress { get; set; } = string.Empty;

    [Display(Name = "Вес груза, кг")]
    public decimal CargoWeightKg { get; set; }

    [Required(ErrorMessage = "Укажите дату забора груза")]
    [DataType(DataType.Date)]
    [Display(Name = "Дата забора груза")]
    public DateTime PickupDate { get; set; } = DateTime.Today;

    [Display(Name = "Дата создания")]
    public DateTime CreatedAtUtc { get; set; }
}
