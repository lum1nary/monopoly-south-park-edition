using UnityEngine;
using System.Collections;

public class CardInfo : BaseCardInfo {

	public int PurchasePrice {get; set;}
	public int LayPrice {get;set;}
	public int SitePrice {get;set;}
	public int H1_Price {get;set;}
	public int H2_Price {get;set;}
	public int H3_Price {get;set;}
	public int H4_Price {get;set;}
	public int Hotel_Price {get;set;}
	public int Position {get;set;}

	public CardInfo(string title, int pPrice, int sPrice, int h1_price, int h2_price, int h3_price, int h4_price, int hotel_price, Group gr, int position)
	:base(title,gr)
	{
		PurchasePrice = pPrice;
		LayPrice = PurchasePrice / 2;
		SitePrice = sPrice;
		H1_Price = h1_price;
		H2_Price = h2_price;
		H3_Price = h3_price;
		H4_Price = h4_price;
		Hotel_Price = hotel_price;
		Position = position;
	}

}
