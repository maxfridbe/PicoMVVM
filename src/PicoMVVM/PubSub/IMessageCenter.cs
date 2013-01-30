using System;

namespace PicoMVVM.PubSub
{
	public interface IMessageCenter
	{
		/// <summary>
		/// Wipes all subscriptions
		/// </summary>
		void ClearSubscriptions();

		/// <summary>
		/// Publish to all who subscribe to this type and topic
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="message"></param>
		/// <param name="topic"></param>
		void Publish<T>(T message, string topic);

		/// <summary>
		/// Publish to no topics who subscribe to this type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="message"></param>
		void Publish<T>(T message);

		/// <summary>
		/// subscribe to all messages of this type
		/// ignore topic
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="action"></param>
		void Subscribe<T>(Action<T> action);

		/// <summary>
		/// subscribe to all topical messages of this type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="topic"></param>
		/// <param name="action"></param>
		void Subscribe<T>(Action<T> action, string topic);

		/// <summary>
		/// Unsubscribe from all messages (NOT FROM TOPICAL messages)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="action"></param>
		void Unsubscribe<T>(Action<T> action);

		/// <summary>
		/// Unsubscribe from topical messages of this type
		/// (NOT FROM ALL MESSAGES)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="topic"></param>
		/// <param name="action"></param>
		void UnSubscribe<T>(Action<T> action, string topic);
	}
}