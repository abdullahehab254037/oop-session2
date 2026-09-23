namespace ConsoleApp12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DeliveryCenter deliveryCenter = new DeliveryCenter();
            //for(int i = 0;i<3;i++)
            //{
            //    Console.WriteLine("enter city");
            //    string city = Console.ReadLine();
            //    Console.WriteLine("enter street");
            //    string street = Console.ReadLine();
            //    Console.WriteLine("enter building number");
            //    int buldingnum=Convert.ToInt32(Console.ReadLine());
            //    DeliveryAddress deliveryAddress = new DeliveryAddress(city,street,buldingnum);
            //    Console.WriteLine("enter code");
            //    string code = Console.ReadLine();
            //    Console.WriteLine("enter descr");
            //    string descr = Console.ReadLine();
            //    Console.WriteLine("enter weight");
            //    int weight = Convert.ToInt32(Console.ReadLine());
            //    Console.WriteLine("enter fee");
            //    int fee = Convert.ToInt32(Console.ReadLine());
            //    Shipment shipment=new Shipment(code,descr,weight,fee,deliveryAddress);
            //    deliveryCenter[i] = shipment;
            //   // deliveryCenter.AddShipment(shipment);
            //}
            //for(int i=0;i<3;i++)
            //{
            //    deliveryCenter[i].PrintShipment();
            //}
            //
            //Console.WriteLine("enter code");
            //string code = Console.ReadLine();
            //Shipment FoundedShipment = deliveryCenter[code];
            //if (FoundedShipment != null) { FoundedShipment.PrintShipment(); }

        }
    }
    public struct DeliveryAddress
    {
        public string city;
        public string street;
        public int BuildingNumber;
        public DeliveryAddress(string city, string street, int buildingNumber)
        {
            this.city = city;
            this.street = street;
            BuildingNumber = buildingNumber;
        }
        public string GetFullAddress()
        {
            return $"{BuildingNumber} {street}, {city}";
        }
    }

    public class Shipment
    {
        private string TrackingCode;
        private string Description;
        private int Weight;
        private int DeliveryFee;
        public DeliveryAddress Destination;

        public Shipment(string trackingCode, string description, int weight, int deliveryFee, DeliveryAddress destination)
        {
            TrackingCode = trackingCode;
            Description = description;
            Weight = weight;
            DeliveryFee = deliveryFee;
            Destination = destination;
        }
        public string code
        {
            get
            {
                return TrackingCode;
            }

        }
        public string description
        {
            get { return Description; }
            set
            {
                if (value != null) { Description = value; }
            }
        }
        public int weight
        {
            get { return Weight; }
            set { if (value > 0) { Weight = value; } }
        }
        public int deliveryFee
        {
            get { return DeliveryFee; }
            private set { if (value > 0) { DeliveryFee = value; } }
        }
        public DeliveryAddress deliveryAddress { get; set; }
        public int EstimatedCost
        {
            get { return DeliveryFee + (Weight * 5); }
        }
        public void UpdateDeliveryFee(int newFee)
        {
            if (newFee > 0)
            {
                DeliveryFee = newFee;
            }
        }
        public void PrintShipment()
        {
            Console.WriteLine($"tracking code:{TrackingCode},,,,description:{Description},,,,weight:{Weight},,,,delivery fee:{DeliveryFee}");
        }
        public Shipment(string trackingCode)
        {
            this.TrackingCode = trackingCode;
            Description = "Unknown";
            Weight = 1;
            DeliveryFee = 50;
            Destination = default;
        }



    }
    public class DeliveryCenter
    {
        private Shipment[] shipments;
        private int count; 
        public int Capacity = 3;
        public DeliveryCenter()
        {
            shipments = new Shipment[Capacity];
            count = 0;
        }
        public Shipment this[int index]
        {
            get
            {
                if (index < 0 || index >= Capacity)
                    return null; 
                return shipments[index];
            }
            set
            {
                if (index < 0 || index >= Capacity)
                    return; 

                shipments[index] = value;
                count++;
            }
        }
        public Shipment this[string trackingCode]
        {
            get
            {
                for (int i = 0; i < Capacity; i++)
                {
                    if (shipments[i].code == trackingCode)
                        return shipments[i];
                }
                return null; 
            }
        }

        public bool AddShipment(Shipment shipment)
        {
            for (int i = 0; i < Capacity; i++)
            {
               
                if (shipments[i]==null)
                {
                    shipments[i] = shipment;
                    return true;
                }
            }
            return false; 
        }


    }
    public class StandardShipment : Shipment
    {
       public StandardShipment(string trackingCode, string description, int weight, int deliveryFee, DeliveryAddress destination):base( trackingCode,  description,  weight,  deliveryFee,  destination)
        {

        }
    }
    public class ExpressShipment: Shipment
    {
        private decimal ExtraFee;
        public ExpressShipment(string trackingCode, string description, int weight, int deliveryFee, DeliveryAddress destination,decimal extrafee) : base(trackingCode, description, weight, deliveryFee, destination)
        {
            ExtraFee = extrafee;
        }
        public decimal extrafee
        {
            get { return ExtraFee; }
            set { if (extrafee > 0) {  ExtraFee = value; }  }
        }
        public decimal EstimatedCost
        {
            get {return this.EstimatedCost + ExtraFee; }
        }

    }

    public class InternationalShipment : Shipment
    {
        private decimal CustomsFee;
        private string DestinationCountry;
        public InternationalShipment(string trackingCode, string description, int weight, int deliveryFee, DeliveryAddress destination,string destinationCountry,
            decimal customsFee) : base(trackingCode, description, weight, deliveryFee, destination)
        {
            DestinationCountry = destinationCountry;
            CustomsFee=customsFee;
        }
        public string destinationCountry
        {
            get { return DestinationCountry; }
            set
            {
                if (value != null) { DestinationCountry = value; }
            }
        }
        public decimal customsFee
        {
            get { return CustomsFee; }
            set
            {
                if(value>0)
                {
                    CustomsFee = value;
                }
            }
        }
        public decimal EstimatedCost
        {
            get { return this.EstimatedCost + CustomsFee; }
        }
    }
    public class DeliveryCenter2: DeliveryCenter
    {
        private string CenterName;
        
        public DeliveryCenter2(string centername):base()
        {
            CenterName = centername;
            this.Capacity = 20;
        }
        public bool RemoveShipment(string trackingcode)
        {
            Shipment s = this[trackingcode];
            if (s== null) { return true; }
            return false;
            
        }



    }
}
