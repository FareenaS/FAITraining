using DataComponentLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataComponentLib
{
    public interface IUserModule
    {
        void RegisterUser(CustomerTable customer);

        void UpdateUserDetails(string address, string telephone, int cstId);

        List<OrderTable> GetAllOrders(int cstId);
    }

    public interface IOrderModule
    {
        void CreateNewOrder(OrderTable record);

        //void UpdateOrderDetail(int noOfItems, double totalAmount, int orderId);
        void UpdateOrderDetail(int orderId);
    }

    public class UserComponent : IUserModule
    {
        public List<OrderTable> GetAllOrders(int cstId)
        {
            throw new NotImplementedException();
        }
        private bool isCustomerNotAvailable(string emailAddress)
        {
            //get the customer with matching email address
            var contxt = new FaiTrainingEntities();
            var cst = contxt.CustomerTables.FirstOrDefault((c) => c.EmailAddress == emailAddress);
            //if not found return true, else return false. 
            return cst == null;
        }
        public void RegisterUser(CustomerTable customer)
        {
            var context = new FaiTrainingEntities();
            if (isCustomerNotAvailable(customer.EmailAddress))
                context.CustomerTables.Add(customer);
            else
                throw new CustomerAlreadyExistsException("This Email Address is already available");
            context.SaveChanges();
        }

        public void UpdateUserDetails(string address, string telephone, int cstId)
        {
            var context = new FaiTrainingEntities();
            var cst = context.CustomerTables.FirstOrDefault((c) => c.CustomerId == cstId);
            if (cst == null)
                throw new Exception("Customer does not exist");
            cst.CustomerAddress = address;
            cst.Telephone = telephone;
            context.SaveChanges();
        }
    }

    public class OrderComponent : IOrderModule
    {
        public void CreateNewOrder(OrderTable record)
        {
            var context = new FaiTrainingEntities();
            context.OrderTables.Add(record);
            context.SaveChanges();
        }

        public void UpdateOrderDetail(int orderId)
        {
            var context = new FaiTrainingEntities();
            var order = context.OrderTables.FirstOrDefault((c) => c.OrderId == orderId);
            if (order == null)
                throw new Exception("Order does not exist");
            order.TotalItems = order.BillDetailTables.Count;
            order.BillAmount = order.BillDetailTables.Sum((e)=>e.ProductTable.Price * e.ProductTable.ProductCount);
            context.SaveChanges();
        }
    }

    public class CartComponent
    {
        public CustomerTable Customer { get; set; }

        private List<ProductTable> CartItems = new List<ProductTable>();

        public void AddToCart(ProductTable item) => CartItems.Add(item);

        private int getCurrentOrderId()
        {
            var context = new FaiTrainingEntities();
            var orderId = context.OrderTables.Max((o) => o.OrderId);
            return orderId;
        }

        private void addToBillDetail(List<BillDetailTable> items)
        {
            var context = new FaiTrainingEntities();
            context.BillDetailTables.AddRange(items);
            context.SaveChanges();
        }
        public void GenerateBill()
        {
            var context = new FaiTrainingEntities();

            OrderComponent com = new OrderComponent();
            //Create the order
            OrderTable order = new OrderTable();
            if(Customer == null)
            {
                throw new Exception("Login First before U order the products");
            }
            order.CustomerId = Customer.CustomerId;
            order.OrderDate = DateTime.Now;
            com.CreateNewOrder(order);
            //Get the OrderId of this order:
            var currentOrderId = getCurrentOrderId();
            List<BillDetailTable> billingDetails = new List<BillDetailTable>();
            foreach(var item in CartItems)
            {
                var billDetail = new BillDetailTable
                {
                    OrderId = currentOrderId,
                    ProductId = item.ProductId
                };
                billingDetails.Add(billDetail);
            }
            addToBillDetail(billingDetails);
        }
    }
    public class CustomerAlreadyExistsException : Exception
    {
        public CustomerAlreadyExistsException()
        {
        }

        public CustomerAlreadyExistsException(string message) : base(message)
        {
        }

        public CustomerAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
