using System.ComponentModel.DataAnnotations;

namespace DeliveryOrders.ViewModels;

public class CreateOrderViewModel
{
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

    [Required(ErrorMessage = "Укажите вес груза")]
    [Display(Name = "Вес груза, кг")]
    public string CargoWeightKg { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите дату забора груза")]
    [DataType(DataType.Date)]
    [Display(Name = "Дата забора груза")]
    public DateTime PickupDate { get; set; } = DateTime.Today;
}
