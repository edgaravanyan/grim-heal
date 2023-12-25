using System.Collections.Generic;
using Core.Contracts;
using Core.Contracts.Map;
using Core.Contracts.Messages;
using Core.MessagePipe.Messages;
using VContainer;
using VContainer.Unity;

namespace Application.Map
{
    /// <summary>
    /// Controller responsible for managing the map and its associated views.
    /// </summary>
    public class MapController : IMapController, IInitializable
    {
        [Inject] private IMap map;
        [Inject] private IViewProvider<IMapView> mapViewProvider;
        [Inject] private IMessageManager messageManager;

        /// <summary>
        /// Creates initial map chunk views and subscribes to update positions from map.
        /// </summary>
        async void IInitializable.Initialize()
        {
            // List to store map view instances.
            var mapViews = new List<IMapView>();
            
            // Create map view instances based on the count and positions of MapChunks in Map.
            foreach (var mapChunk in map.Chunks)
            {
                var mapView = await mapViewProvider.CreateViewAsync();
                mapView.SetPosition(mapChunk.Position); 
                mapViews.Add(mapView);
            }
            
            // Subscribe to MapChunkMessage events to update the visual representation when a map chunk changes.
            messageManager.Subscribe<MapChunkMessage>(message =>
            {
                var mapChunk = message.Data;
                var chunkPosition = mapChunk.Position;
                mapViews[mapChunk.Id].SetPosition(chunkPosition);
            });
        }
    }
}