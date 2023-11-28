import React, { useRef } from "react";
import { FlatList } from "react-native";

interface ListProps {
  data: any[];
  render: (itemData: any) => React.ReactElement | null;
}

const List: React.FC<ListProps> = ({ data, render }) => {
  const ref = useRef<FlatList>(null);

  return (
    <FlatList
      ref={ref}
      data={data}
      renderItem={({ item }) => render(item)}
      pagingEnabled
      snapToAlignment="center"
      showsVerticalScrollIndicator={false}
    />
  );
};

export default List;
