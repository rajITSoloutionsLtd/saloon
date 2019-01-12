using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace demo
{
	public class Utility
	{
		/// <summary>
		/// Covert a list from a another list
		/// </summary>
		/// <typeparam name="T">From Convert List</typeparam>
		/// <typeparam name="U">To Convert List</typeparam>
		/// <param name="provideLst">Fom Convert List Value</param>
		/// <returns></returns>
		public List<U> ConvertList<T, U>(List<T> provideLst)
		{
			List<U> lst = new List<U>();
			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

			var properties = typeof(U).GetProperties();
			foreach (T itm in provideLst)
			{
				var objT = Activator.CreateInstance<U>();
				foreach (var pro in properties)
				{
					try
					{
						if (props[pro.Name].GetValue(itm) != null && !props[pro.Name].GetValue(itm).Equals(""))
						{
							if (pro.PropertyType == typeof(System.DateTime))
							{
								pro.SetValue(objT, props[pro.Name].GetValue(itm), null);
							}
							else if (pro.PropertyType == typeof(System.Byte[]))
							{
								pro.SetValue(objT, (Byte[])props[pro.Name].GetValue(itm), null);
							}
							else if (pro.PropertyType == typeof(Nullable<System.Int32>))
							{
								pro.SetValue(objT, Convert.ToInt32(props[pro.Name].GetValue(itm).ToString()), null);
							}
							else if (pro.PropertyType == typeof(System.Int32))
							{
								pro.SetValue(objT, Convert.ToInt32(props[pro.Name].GetValue(itm).ToString()), null);
							}
							else if (pro.PropertyType == typeof(System.Double))
							{
								pro.SetValue(objT, Convert.ToDouble(props[pro.Name].GetValue(itm).ToString()), null);
							}
							else if (pro.PropertyType == typeof(Nullable<System.Double>))
							{
								pro.SetValue(objT, Convert.ToDouble(props[pro.Name].GetValue(itm).ToString()), null);
							}
							else if (pro.PropertyType == typeof(System.Boolean))
							{
								pro.SetValue(objT, (Convert.ToBoolean(props[pro.Name].GetValue(itm))), null);
							}
							else if (pro.PropertyType == typeof(System.Decimal))
							{
								pro.SetValue(objT, (Convert.ToDecimal(props[pro.Name].GetValue(itm))), null);
							}
							else if (pro.PropertyType == typeof(Nullable<System.Decimal>))
							{
								pro.SetValue(objT, (Convert.ToDecimal(props[pro.Name].GetValue(itm))), null);
							}
							else if (pro.PropertyType == typeof(Nullable<System.Boolean>))
							{
								pro.SetValue(objT, Convert.ToInt32(props[pro.Name].GetValue(itm)), null);
							}
							else if (pro.PropertyType == typeof(System.Guid))
							{
								pro.SetValue(objT, (new Guid(props[pro.Name].GetValue(itm).ToString())), null);
							}
							else if (pro.PropertyType == typeof(Nullable<System.Guid>))
							{
								pro.SetValue(objT, (new Guid(props[pro.Name].GetValue(itm).ToString())), null);
							}
							else
							{
								pro.SetValue(objT, props[pro.Name].GetValue(itm).ToString(), null);
							}

						}
						else
							pro.SetValue(objT, "", null);



					}
					catch (Exception ex)
					{

					}

				}
				lst.Add(objT);
			}

			return lst;
		}

		
	}
}