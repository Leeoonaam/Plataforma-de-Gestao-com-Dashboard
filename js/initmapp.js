// Initialize and add the map
                                            function initMap2() {


                                                // The location of Uluru
                                                var uluru = { lat: -23.4944863, lng: -46.8537396 };

                                                // The map, centered at Uluru
                                                var map = new google.maps.Map(document.getElementById('map1'), { zoom: 15, center: uluru });// { zoom: 4, center: uluru });

                                                //var marker = new google.maps.Marker({ position: uluru2, icon: '/imagens/ddd.png', map: map, title: 'teste' });

                                                    <%
                                            Response.Write(sPlace_Map_Roteiro);
                                                    %>
                                                    //var uluru2 = { lat: -22.686, lng: -46.619 };
                                                    //var marker = new google.maps.Marker({ position: uluru2, map: map, title: 'teste' });

                                                }
                                            </script>