using System;
using System.ComponentModel;
using System.Configuration;

namespace Com.Xenthrax.RegistrySettings
{
	public sealed class RegistrySettingsProviderContext : ITypeDescriptorContext
	{
		internal RegistrySettingsProviderContext(Type sourceType, Type targetType, object propertyValue, object serializedValue, SettingsProperty property)
		{
			this.SourceType = sourceType;
			this.TargetType = targetType;
			this.PropertyValue = propertyValue;
			this.SerializedValue = serializedValue;
			this.Property = property;
		}

		public Type SourceType { get; private set; }
		public Type TargetType { get; private set; }
		public object PropertyValue { get; private set; }
		public object SerializedValue { get; private set; }
		public SettingsProperty Property { get; private set; }

		IContainer ITypeDescriptorContext.Container
		{
			get { throw new NotImplementedException(); }
		}

		object ITypeDescriptorContext.Instance
		{
			get { throw new NotImplementedException(); }
		}

		void ITypeDescriptorContext.OnComponentChanged()
		{
			throw new NotImplementedException();
		}

		bool ITypeDescriptorContext.OnComponentChanging()
		{
			throw new NotImplementedException();
		}

		PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
		{
			get { throw new NotImplementedException(); }
		}

		object IServiceProvider.GetService(Type serviceType)
		{
			throw new NotImplementedException();
		}
	}
}