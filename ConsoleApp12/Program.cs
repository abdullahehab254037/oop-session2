namespace ConsoleApp12
{
    internal class Program
    {
        static void Main(string[] args)
        {

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
        private int Capacity = 3;
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
               
                if (shipments[i].code == null)
                {
                    shipments[i] = shipment;
                    return true;
                }
            }
            return false; 
        }


    }
}
