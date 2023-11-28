import React from "react";
import List from "../List";
import DefaultScreenStructure from "../DefaultScreenStructure";

interface ListScreenStructureProps {
  title: string;
  headerButton?: React.ReactNode;
  listData: any[];
  listRender: (itemData: any) => React.ReactElement | null;
}

const ListScreenStructure: React.FC<ListScreenStructureProps> = ({
  headerButton,
  listData,
  listRender,
  title,
}) => {
  return (
    <DefaultScreenStructure title={title} headerButton={headerButton}>
      <List data={listData} render={listRender} />
    </DefaultScreenStructure>
  );
};

export default ListScreenStructure;
