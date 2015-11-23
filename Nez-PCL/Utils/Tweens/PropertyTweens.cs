﻿using System;
using System.Collections;
using System.Reflection;
using Microsoft.Xna.Framework;


namespace Nez.Tweens
{
	/// <summary>
	/// generic ITweenTarget used for all property tweens
	/// </summary>
	class PropertyTarget<T> : ITweenTarget<T> where T : struct
	{
		protected object _target;
		FieldInfo _fieldInfo;
		protected Action<T> _setter;
		protected Func<T> _getter;


		public void setTweenedValue( T value )
		{
			if( _fieldInfo != null )
				_fieldInfo.SetValue( _target, value );
			else
				_setter( value );
		}


		public T getTweenedValue()
		{
			if( _fieldInfo != null )
				return (T)_fieldInfo.GetValue( _target );
			return _getter();
		}


		public PropertyTarget( object target, string propertyName )
		{
			_target = target;

			// try to fetch the field. if we dont find it this is a property
			if( ( _fieldInfo = target.GetType().GetField( propertyName ) ) == null )
			{
				_setter = ReflectionUtils.setterForProperty<Action<T>>( target, propertyName );
				_getter = ReflectionUtils.getterForProperty<Func<T>>( target, propertyName );
			}

			Debug.assertIsTrue( _setter != null || _fieldInfo != null, "either the property (" + propertyName + ") setter or getter could not be found on the object " + target );
		}


		public object getTargetObject()
		{
			return _target;
		}
	}


	public static class PropertyTweens
	{
		public static ITween<int> intPropertyTo( object self, string propertyName, int to, float duration )
		{
			var tweenTarget = new PropertyTarget<int>( self, propertyName );
			var tween = TweenManager.cacheIntTweens ? QuickCache<IntTween>.pop() : new IntTween();
			tween.initialize( tweenTarget, to, duration );

			return tween;
		}


		public static ITween<float> floatPropertyTo( object self, string propertyName, float to, float duration )
		{
			var tweenTarget = new PropertyTarget<float>( self, propertyName );
			var tween = TweenManager.cacheFloatTweens ? QuickCache<FloatTween>.pop() : new FloatTween();
			tween.initialize( tweenTarget, to, duration );

			return tween;
		}


		public static ITween<Vector2> vector2PropertyTo( object self, string propertyName, Vector2 to, float duration )
		{
			var tweenTarget = new PropertyTarget<Vector2>( self, propertyName );
			var tween = TweenManager.cacheVector2Tweens ? QuickCache<Vector2Tween>.pop() : new Vector2Tween();
			tween.initialize( tweenTarget, to, duration );

			return tween;
		}


		public static ITween<Vector3> vector3PropertyTo( object self, string propertyName, Vector3 to, float duration )
		{
			var tweenTarget = new PropertyTarget<Vector3>( self, propertyName );
			var tween = TweenManager.cacheVector3Tweens ? QuickCache<Vector3Tween>.pop() : new Vector3Tween();
			tween.initialize( tweenTarget, to, duration );

			return tween;
		}


		public static ITween<Vector4> vector4PropertyTo( object self, string propertyName, Vector4 to, float duration )
		{
			var tweenTarget = new PropertyTarget<Vector4>( self, propertyName );
			var tween = TweenManager.cacheVector4Tweens ? QuickCache<Vector4Tween>.pop() : new Vector4Tween();
			tween.initialize( tweenTarget, to, duration );

			return tween;
		}


		public static ITween<Quaternion> quaternionPropertyTo( object self, string propertyName, Quaternion to, float duration )
		{
			var tweenTarget = new PropertyTarget<Quaternion>( self, propertyName );
			var tween = TweenManager.cacheQuaternionTweens ? QuickCache<QuaternionTween>.pop() : new QuaternionTween();
			tween.initialize( tweenTarget, to, duration );

			return tween;
		}


		public static ITween<Color> colorPropertyTo( object self, string propertyName, Color to, float duration )
		{
			var tweenTarget = new PropertyTarget<Color>( self, propertyName );
			var tween = TweenManager.cacheColorTweens ? QuickCache<ColorTween>.pop() : new ColorTween();
			tween.initialize( tweenTarget, to, duration );

			return tween;
		}

	}
}
