using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PicoMVVM.Infrastructure.Options;

namespace PicoMVVM.PubSub
{
	/// <summary>
	/// Allows for topical typesafe message passing
	/// </summary>
	public class MessageCenter : IMessageCenter
	{
		public MessageCenter()
		{
			_messageBus = new Dictionary<Type, Dictionary<Option<string>, List<Delegate>>>();
		}

		readonly Dictionary<Type, Dictionary<Option<string>, List<Delegate>>> _messageBus;

		/// <summary>
		/// Wipes all subscriptions
		/// </summary>
		public void ClearSubscriptions()
		{
			_messageBus.Clear();
		}

		/// <summary>
		/// Publish to all who subscribe to this type and topic
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="message"></param>
		/// <param name="topic"></param>
		public void Publish<T>(T message, string topic)
		{
			publishInternal(message, Option.Some(topic));
		}

		/// <summary>
		/// Publish to non topics who subscribe to this type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="message"></param>
		public void Publish<T>(T message)
		{
			publishInternal(message, Option.None<string>());
		}

		/// <summary>
		/// subscribe to all messages of this type
		/// ignore topic
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="action"></param>
		public void Subscribe<T>(Action<T> action)
		{
			subscribeInternal(Option.None<string>(), action);
		}

		/// <summary>
		/// subscribe to all topical messages of this type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="topic"></param>
		/// <param name="action"></param>
		public void Subscribe<T>(Action<T> action, string topic)
		{
			subscribeInternal(Option.Some(topic), action);
		}

		/// <summary>
		/// Unsubscribe from all messages (NOT FROM TOPICAL messages)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="action"></param>
		public void Unsubscribe<T>(Action<T> action)
		{
			unSubscribeInternal(Option.None<string>(), action);
		}

		/// <summary>
		/// Unsubscribe from topical messages of this type
		/// (NOT FROM ALL MESSAGES)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="topic"></param>
		/// <param name="action"></param>
		public void UnSubscribe<T>(Action<T> action, string topic)
		{
			unSubscribeInternal(Option.Some(topic), action);
		}


		private void publishInternal<T>(T message, Option<string> topic)
		{
			var type = typeof(T);
			if (!_messageBus.ContainsKey(type))
				return;

			if (topic.IsSome() && _messageBus[type].ContainsKey(topic))
			{
				foreach (var delg in _messageBus[type][topic])
				{
					delg.DynamicInvoke(message);
				}
			}


			if (_messageBus[type].ContainsKey(Option.None<string>()))
			{
				foreach (var delg in _messageBus[type][Option.None<string>()])
				{
					delg.DynamicInvoke(message);
				}
			}



		}

		private void subscribeInternal<T>(Option<string> topic, Action<T> action)
		{
			var type = typeof(T);
			if (_messageBus.ContainsKey(type))
			{
				var topicActionDictionary = _messageBus[type];
				if (topicActionDictionary.ContainsKey(topic))
				{
					topicActionDictionary[topic].Add(action);
				}
				else
				{
					topicActionDictionary.Add(topic, new List<Delegate> { action });
				}
			}
			else
			{
				_messageBus.Add(typeof(T), new Dictionary<Option<string>, List<Delegate>>
	                                        {
	                                            { topic , new List<Delegate> {action} }
	                                        });
			}
		}

		private void unSubscribeInternal<T>(Option<string> topic, Action<T> action)
		{
			var type = typeof(T);
			if (_messageBus.ContainsKey(type))
			{
				var topicActionDictionary = _messageBus[type];
				if (topicActionDictionary.ContainsKey(topic))
				{
					topicActionDictionary[topic].Remove(action);
				}
			}
		}
	}
}
