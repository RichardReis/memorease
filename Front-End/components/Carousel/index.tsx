import React, { useRef, useState } from "react";
import { FlatList } from "react-native";

interface SnapCarouselProps {
  data: any[];
  render: (itemData: any) => React.ReactElement | null;
}

const SnapCarousel: React.FC<SnapCarouselProps> = ({ data, render }) => {
  const [index, setIndex] = useState(0);
  const ref = useRef<FlatList>(null);

  const handleOnViewableItemsChanged = useRef(({ viewableItems }: any) => {
    if (viewableItems?.length > 0) {
      setIndex(viewableItems[0].index);
    }
  }).current;

  const viewabilityConfig = useRef({
    itemVisiblePercentThreshold: 50,
  }).current;

  return (
    <FlatList
      ref={ref}
      data={data}
      renderItem={({ item }) => render(item)}
      horizontal
      pagingEnabled
      snapToAlignment="center"
      showsHorizontalScrollIndicator={false}
      onViewableItemsChanged={handleOnViewableItemsChanged}
      viewabilityConfig={viewabilityConfig}
    />
  );
};

export default SnapCarousel;
