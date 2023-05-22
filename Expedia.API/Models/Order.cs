using System;
using System.ComponentModel.DataAnnotations;
using Stateless;

namespace Expedia.API.Models
{
    public enum OrderStateEnum
    {
        Pending,
        Processing,
        Completed,
        Declined,
        Cancelled,
        Refund,
    }

    public enum OrderStateTriggerEnum
    {
        PlaceOrder,
        Approve,
        Reject,
        Cancel,
        Return,
    }

	public class Order
	{
        public Order()
        {
            StateMachineInit();
        }

        [Key]
        public Guid Id { get; set; }


        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        public ICollection<LineItem> OrderItems { get; set; }

        public OrderStateEnum State { get; set; }

        public DateTime CreateDateUTC { get; set; }

        public string? TransactionMetadata { get; set; }

        StateMachine<OrderStateEnum, OrderStateTriggerEnum> _machine;

        private void StateMachineInit()
        {
            _machine = new StateMachine<OrderStateEnum, OrderStateTriggerEnum>(
                OrderStateEnum.Pending
                );
            // pending
            _machine.Configure(OrderStateEnum.Pending)
                .Permit(
                    OrderStateTriggerEnum.PlaceOrder, OrderStateEnum.Processing
                )
                .Permit(
                    OrderStateTriggerEnum.Cancel, OrderStateEnum.Cancelled
                );
            // processing
            _machine.Configure(OrderStateEnum.Processing)
                .Permit(
                    OrderStateTriggerEnum.Approve, OrderStateEnum.Completed
                )
                .Permit(
                    OrderStateTriggerEnum.Reject, OrderStateEnum.Declined
                );
            // declined
            _machine.Configure(OrderStateEnum.Declined)
                .Permit(
                    OrderStateTriggerEnum.PlaceOrder, OrderStateEnum.Processing
                );
            // completed
            _machine.Configure(OrderStateEnum.Completed)
                .Permit(
                    OrderStateTriggerEnum.Return, OrderStateEnum.Refund
                );
        }
    }
}

