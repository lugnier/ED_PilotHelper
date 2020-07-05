using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Utils
{
    public static class MessageCenter
    {
        /// <summary>
        /// Liste des messages sans paramètres
        /// Item1 : String : Id du message
        /// Item2 : Object : instance du souscripteur
        /// Item3 : Action : méthode anonyme 
        /// </summary>
        static List<Tuple<String, Object, Action>> _messages = new List<Tuple<string, object, Action>>();

        /// <summary>
        /// Liste des messages avec paramètre
        /// Item1 : String : Id du message
        /// Item2 : Object : instance du souscripteur
        /// Item3 : Action(Object) : méthode anonyme 
        /// </summary>
        static List<Tuple<String, Object, Action<Object>>> _messagesWithParam =
            new List<Tuple<string, object, Action<Object>>>();

        /// <summary>
        /// recuperation de tous les messages
        /// </summary>
        /// <returns></returns>
        public static List<Tuple<String, String, Boolean>> ListOfAllMessages()
        {
            List<Tuple<String, String, Boolean>> l = new List<Tuple<string, string, bool>>();

            foreach (Tuple<string, object, Action> message in _messages)
            {
                l.Add(new Tuple<string, string, bool>(message.Item1,
                    message.Item2 != null ? message.Item2.ToString() : "", false));
            }

            foreach (Tuple<string, object, Action<object>> message in _messagesWithParam)
            {
                l.Add(new Tuple<string, string, bool>(message.Item1,
                    message.Item2 != null ? message.Item2.ToString() : "", true));
            }

            foreach (Tuple<string, string, bool> tuple in l)
            {
                Debug.WriteLine($"{tuple.Item1} - {tuple.Item2} - {tuple.Item3}");
            }

            return l;
        }

        /// <summary>
        /// abonnement au message
        /// </summary>
        /// <param name="idMessage">String nom du message</param>
        /// <param name="callback">Méthode sans paramètre à exécuter lors de l'appel</param>
        public static void Subscribe(String idMessage, Action callback)
        {

            // on vérifie que l'id de message n'est pas déjà utilisé dans la liste des messages avec paramètre

            for (int i = _messages.Count - 1; i >= 0; i--)
            {
                if (_messages[i].Item1 == idMessage)
                {
                    try
                    {
                        _messages.RemoveAt(i);
                    }
                    catch (Exception)
                    {
                        // catch blanc
                    }

                }
            }


            if (_messages.All(x => x.Item1 != idMessage))
            {
                _messages.Add(new Tuple<string, object, Action>(idMessage, null, callback));
            }
            else
            {
                throw new FieldAccessException(
                    "La réinitialisation du tableau des messages ne s'est pas bien passé");
            }
        }

        /// <summary>
        /// abonnement au message
        /// </summary>
        /// <param name="idMessage">String nom du message</param>
        /// <param name="callback">Méthode avec paramètre à exécuter lors de l'appel</param>
        public static void Subscribe(String idMessage, Action<object> callback)
        {

            for (int i = _messagesWithParam.Count - 1; i >= 0; i--)
            {
                if (_messagesWithParam[i].Item1 == idMessage)
                {
                    try
                    {
                        _messagesWithParam.RemoveAt(i);
                    }
                    catch (Exception)
                    {
                        // catch blanc
                    }

                }
            }



            if (_messagesWithParam.All(x => x.Item1 != idMessage))
            {
                _messagesWithParam.Add(new Tuple<string, object, Action<object>>(idMessage, null, callback));
            }
            else
            {
                throw new FieldAccessException(
                    "La réinitialisation du tableau des messages avec paramètres ne s'est pas bien passé");
            }
        }

        /// <summary>
        /// abonnement au message
        /// </summary>
        /// <param name="subscriber">Object référence du souscripteur</param>
        /// <param name="idMessage">String nom du message</param>
        /// <param name="callback">Méthode sans paramètre à exécuter lors de l'appel</param>
        public static void Subscribe(Object subscriber, String idMessage, Action callback)
        {

            // on vérifie que l'id de message n'est pas déjà utilisé dans la liste des messages avec paramètre
            while (_messages.Any(x => x.Item2 != null && x.Item2.Equals(subscriber) && x.Item1 == idMessage))
            {
                try
                {
                    _messages.Remove(_messages.First(x =>
                        x.Item2 != null && x.Item2.Equals(subscriber) && x.Item1 == idMessage));
                }
                catch (Exception)
                {
                    // catch blanc
                }
            }

            if (!_messages.Any(x => x.Item2 != null && x.Item2.Equals(subscriber) && x.Item1 == idMessage))
            {
                _messages.Add(new Tuple<string, object, Action>(idMessage, subscriber, callback));
            }
            else
            {
                throw new FieldAccessException(
                    "La réinitialisation du tableau des messages avec subscriber ne s'est pas bien passé");
            }
        }

        /// <summary>
        /// abonnement au message
        /// </summary>
        /// <param name="subscriber">Object référence du souscripteur</param>
        /// <param name="idMessage">String nom du message</param>
        /// <param name="callback">Méthode avec paramètre à exécuter lors de l'appel</param>
        public static void Subscribe(Object subscriber, String idMessage, Action<object> callback)
        {
            // suppression de tous les messages qui resteraient dans la liste
            while (_messagesWithParam.Any(
                x => x.Item2 != null && x.Item2.Equals(subscriber) && x.Item1 == idMessage))
            {
                try
                {
                    _messagesWithParam.Remove(_messagesWithParam.First(x =>
                        x.Item2 != null && x.Item2.Equals(subscriber) && x.Item1 == idMessage));
                }
                catch (Exception)
                {
                    // catch blanc
                }
            }

            // insertion du message et de la méthode anonyme
            if (!_messagesWithParam.Any(x => x.Item2 != null && x.Item2.Equals(subscriber) && x.Item1 == idMessage))
            {
                _messagesWithParam.Add(new Tuple<string, object, Action<object>>(idMessage, subscriber, callback));
            }
            else
            {
                throw new FieldAccessException(
                    "La réinitialisation du tableau des messages avec paramètre et avec subscriber ne s'est pas bien passé");
            }
        }

        /// <summary>
        /// Désabonnement d'un message
        /// </summary>
        /// <param name="idMessage">String : nom du message</param>
        public static void Unsubscribe(String idMessage)
        {
            Tuple<string, object, Action> r = _messages.FirstOrDefault(x => x.Item1 == idMessage);
            if (r != null)
            {
                try
                {
                    _messages.Remove(r);
                }
                catch (Exception)
                {
                    // catch blanc
                }
            }

            Tuple<string, object, Action<object>> r2 = _messagesWithParam.FirstOrDefault(x => x.Item1 == idMessage);
            if (r2 != null)
            {
                try
                {
                    _messagesWithParam.Remove(r2);
                }
                catch (Exception)
                {
                    // catch blanc
                }
            }
        }

        public static void UnsubscribeAll()
        {
            for (int i = _messages.Count - 1; i >= 0; i--)
            {
                _messages.RemoveAt(i);
            }

            for (int i = _messagesWithParam.Count - 1; i >= 0; i--)
            {
                _messagesWithParam.RemoveAt(i);
            }

            GC.Collect();
        }

        /// <summary>
        /// Désabonnement d'un message pour un souscripteur uniquement
        /// </summary>
        /// <param name="subscriber">Object référence du souscripteur</param>
        /// <param name="idMessage">String : nom du message</param>
        public static void Unsubscribe(Object subscriber, String idMessage)
        {
            //var r = Messages.Where(x => x.Item2.Equals(subscriber) && x.Item1 == idMessage).FirstOrDefault();
            Tuple<string, object, Action> r = _messages.FirstOrDefault(x =>
                x.Item2 != null && x.Item2.Equals(subscriber) && x.Item1 == idMessage);
            if (r != null)
            {
                try
                {
                    _messages.Remove(r);
                }
                catch (Exception)
                {
                    // catch blanc
                }
            }

            Tuple<string, object, Action<object>> r2 = _messagesWithParam.FirstOrDefault(x =>
                x.Item2 != null && x.Item2.Equals(subscriber) && x.Item1 == idMessage);
            if (r2 == null) return;
            try
            {
                _messagesWithParam.Remove(r2);
            }
            catch (Exception)
            {
                //catch blanc
            }
        }

        /// <summary>
        /// envoi du message pour déclancher la méthode associée
        /// </summary>
        /// <param name="idMessage">String : nom du message</param>
        public static void Send(String idMessage)
        {
            foreach (Tuple<string, object, Action> item in _messages.Where(x => x.Item1 == idMessage))
            {
                item.Item3.Invoke();
            }


        }

        /// <summary>
        /// envoi du message pour déclancher la méthode associée a un souscripteur uniquement
        /// </summary>
        /// <param name="idMessage">String : nom du message</param>
        public static void SendToSubscriber(Object subscriber, String idMessage)
        {
            foreach (Tuple<string, object, Action> item in _messages.Where(x =>
                x.Item1 == idMessage && x.Item2.Equals(subscriber)))
            {
                item.Item3.Invoke();
            }


        }

        /// <summary>
        /// envoi du message pour déclancher la méthode associée
        /// </summary>
        /// <param name="idMessage">String : nom du message</param>
        /// <param name="param">paramètre à passer (faiblement typé)</param>
        public static void Send(String idMessage, object param)
        {


            try
            {
                foreach (Tuple<string, object, Action<object>> item in _messagesWithParam.Where(x =>
                    x.Item1 == idMessage))
                {
                    item.Item3.Invoke(param);
                }
            }
            catch (Exception e)
            {
                //Utilitaire.Log(4039, Utilitaire.enumGravite.grave, $"idMessage {idMessage ?? "???"} param {param ?? "???"}", e);
                throw;
            }
        }

        /// <summary>
        /// envoi du message pour déclancher la méthode associée
        /// </summary>
        /// <param name="idMessage">String : nom du message</param>
        /// <param name="param">paramètre à passer (faiblement typé)</param>
        public static void SendToSubscriber(Object subscriber, String idMessage, object param)
        {


            foreach (Tuple<string, object, Action<object>> item in _messagesWithParam.Where(x =>
                x.Item1 == idMessage && x.Item2.Equals(subscriber)))
            {
                item.Item3.Invoke(param);
            }
        }

    }
}

