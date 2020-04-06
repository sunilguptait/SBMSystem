using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Common;
using BMS.Data;
using BMS.Data.Entities;
using BMS.ViewModels.Book;
using BMS.ViewModels.Order;
using BMS.Common.Extensions;
using System.Data;

namespace BMS.Services.Order
{
    public class OrderService : IOrderService
    {
        BMSContext bMSContext = new BMSContext();

        public OrderMaster GetClassById(int id)
        {
            return bMSContext.OrderMaster.Where(m => m.Order_Id == id).FirstOrDefault();
        }

        public OrderVM GetOrderWithDetailsByIdOrCode(int id = 0, string orderCode = "")
        {
            var order = (from OM in bMSContext.OrderMaster
                         where (id == 0 || OM.Order_Id == id) && (string.IsNullOrEmpty(orderCode) || OM.Order_Code == orderCode)
                         select new OrderVM()
                         {
                             OrderId = OM.Order_Id,
                             OrderCode = OM.Order_Code,
                             OrderDate = OM.Order_date,
                             OrderPaymentMode = OM.Order_PaymentMode,
                             PaymentStatus = OM.Order_PaymentStatus,
                             StudentEnrollmentId = OM.Order_SEId,
                             OrderStatus = OM.Order_Status,
                             TotalOrderAmount = OM.Order_TotalAmount,
                             QRCode = OM.Order_QRCode,
                             OrderPaymentRemark = OM.Order_PaymentRemark
                         }).FirstOrDefault();

            if (order != null)
            {
                var orderBooks = (from ODM in bMSContext.OrderDetailMaster
                                  join BM in bMSContext.BookMaster on ODM.OD_BookId equals BM.Book_Id
                                  where ODM.OD_OrderId == order.OrderId
                                  select new BookMasterVM()
                                  {
                                      Book_Id = BM.Book_Id,
                                      Book_Name = BM.Book_Name,
                                      Book_Price = ODM.OD_Price,
                                      Quantity = ODM.OD_Qty,
                                      Book_Image = BM.Book_Image,
                                      TotalAmount = ODM.OD_Total,
                                      Book_ShortName = BM.Book_ShortName
                                  }).ToList();
                order.Books = orderBooks;
                if (order.Books != null)
                {
                    int rn = 1;
                    order.Books.ForEach(m => { m.RN = rn++; });
                }
            }
            return order;
        }

        public Tuple<OrderMaster, string> SaveOrder(OrderVM model)
        {
            using (DbContextTransaction transaction = bMSContext.Database.BeginTransaction())
            {
                try
                {
                    //Save Order
                    var entity = new OrderMaster();
                    entity.Order_date = model.OrderDate;
                    entity.Order_SEId = Convert.ToInt32(model.StudentEnrollmentId);
                    entity.Order_Status = Convert.ToInt32(model.OrderStatus);
                    entity.Order_PaymentMode = Convert.ToInt32(model.OrderPaymentMode);
                    entity.Order_PaymentStatus = Convert.ToInt32(model.PaymentStatus);
                    entity.Order_TotalAmount = Convert.ToInt32(model.TotalOrderAmount);
                    entity.Order_BSId = model.BookSellerId;
                    entity.Order_PaymentRemark = model.OrderPaymentRemark;
                    bMSContext.OrderMaster.Add(entity);

                    bMSContext.SaveChanges();
                    entity.Order_Code = entity.Order_Id.ToString("0000000");
                    entity.Order_QRCode = CommonMethods.GenerateQRCode(entity.Order_Code);
                    // Save Order Details
                    foreach (var item in model.Books)
                    {
                        var orderDetails = new OrderDetailMaster()
                        {
                            OD_OrderId = entity.Order_Id,
                            OD_BookId = item.Book_Id,
                            OD_Qty = item.Quantity,
                            OD_Total = item.TotalAmount,
                            OD_Price = item.Book_Price,
                            OD_Discount = item.Discount,
                            OD_IsDelete = false,
                            OD_CreatedBy = model.UserId,
                            OD_CreationDate = model.OrderDate
                        };
                        bMSContext.OrderDetailMaster.Add(orderDetails);
                    }
                    bMSContext.SaveChanges();
                    transaction.Commit();
                    return new Tuple<OrderMaster, string>(entity, "");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Tuple<OrderMaster, string>(null, "Error occurred while order generation. Please try again and contact to system admin.");
                }
            }
        }
        public Tuple<OrderMaster, string> UpdateOrder(OrderVM model)
        {
            using (DbContextTransaction transaction = bMSContext.Database.BeginTransaction())
            {
                try
                {
                    var orderEntity = bMSContext.OrderMaster.Where(a => a.Order_Id == model.OrderId).FirstOrDefault();
                    if (orderEntity != null && orderEntity.Order_BSId == model.BookSellerId)
                    {
                        if (!string.IsNullOrEmpty(model.ReceiverSignature))
                        {
                            string signaturePath = CommonMethods.SaveFileFromBase64String("/Content/Images/Signature", model.ReceiverSignature);
                            orderEntity.Order_ReceiverSignature = signaturePath;
                        }
                        orderEntity.Order_Status = model.OrderStatus;
                        orderEntity.Order_PaymentStatus = model.PaymentStatus;
                        bMSContext.SaveChanges();
                        transaction.Commit();
                        return new Tuple<OrderMaster, string>(orderEntity, "");
                    }
                    else
                    {
                        return new Tuple<OrderMaster, string>(null, "Invalid order details. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Tuple<OrderMaster, string>(null, "Error occurred while order updation. Please try again and contact to system admin.");
                }
            }
        }
        public Tuple<List<OrderListVM>, int> GetOrders(OrderSearchVM searchModel)
        {
            var bookSellerId = new SqlParameter { ParameterName = "@BookSellerId", Value = searchModel.BookSellerId };
            var deliveryStatus = new SqlParameter { ParameterName = "@DeliveryStatus", Value = searchModel.DeliveryStatus };
            var paymentStatus = new SqlParameter { ParameterName = "@PaymentStatus", Value = searchModel.PaymentStatus };
            var studentName = new SqlParameter { ParameterName = "@StudentName", Value = searchModel.StudentName };
            var orderNumber = new SqlParameter { ParameterName = "@OrderNumber", Value = (object)searchModel.OrderNumber ?? DBNull.Value };
            var orderDate = new SqlParameter { ParameterName = "@OrderDate", Value = (object)searchModel.OrderDate ?? DBNull.Value };
            var sessionId = new SqlParameter { ParameterName = "@SessionId", Value = (object)searchModel.SessionId ?? DBNull.Value };

            var pageIndex = new SqlParameter { ParameterName = "@PageIndex", Value = searchModel.PageIndex };
            var pageSize = new SqlParameter { ParameterName = "@PageSize", Value = searchModel.PageSize };
            var orderBy = new SqlParameter { ParameterName = "@OrderBy", Value = (object)searchModel.SortBy ?? DBNull.Value };
            var orderDirection = new SqlParameter { ParameterName = "@OrderDirection", Value = (object)searchModel.SortDirection ?? DBNull.Value };

            var totalRecords = new SqlParameter { ParameterName = "@TotalRecords", Value = 0, Direction = ParameterDirection.Output };

            var orders = bMSContext.Database.SqlQuery<OrderListVM>("EXEC GetOrdersList @BookSellerId,@DeliveryStatus,@PaymentStatus,@StudentName,@OrderNumber,@OrderDate,@SessionId,@PageIndex,@PageSize,@OrderBy,@OrderDirection,@TotalRecords OUTPUT",
                    bookSellerId, deliveryStatus, paymentStatus, studentName, orderNumber, orderDate, sessionId, pageIndex, pageSize, orderBy, orderDirection, totalRecords).ToList();

            return new Tuple<List<OrderListVM>, int>(orders, Convert.ToInt32(totalRecords.Value));
        }

        public List<InvoiceVM> GetInvoices(InvoiceInputVM searchModel)
        {
            string orderCodesXml = searchModel.Orders.ToXml<List<string>>();
            var bookSellerId = new SqlParameter { ParameterName = "@BookSellerId", Value = searchModel.BookSellerId };
            var orderCodes = new SqlParameter { ParameterName = "@OrderCodes", Value = orderCodesXml };
            var sessionId = new SqlParameter { ParameterName = "@SessionId", Value = searchModel.SessionId };
            var orders = bMSContext.Database.SqlQuery<InvoiceVM>("EXEC GetInvoiceList @BookSellerId,@OrderCodes,@SessionId",
                    bookSellerId, orderCodes, sessionId).ToList();
            return orders;
        }

    }
}
