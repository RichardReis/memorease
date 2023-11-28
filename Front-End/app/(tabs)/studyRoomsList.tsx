import React, { useEffect, useState } from "react";
import { StyleSheet, View, Text, TouchableOpacity } from "react-native";
import Icon from "../../components/Icon";
import { useRouter } from "expo-router";
import ListScreenStructure from "../../components/ListScreenStructure";
import Button from "../../components/Button";
import Colors from "../../constants/Colors";
import Spacing from "../../constants/Spacing";
import TextStyles from "../../themedStyles/Text";
import DeleteModal from "../../components/DeleteModal";
import { LoadRooms, RoomDataList } from "../../service/Room/loadRooms";
import { RemoveRoom } from "../../service/Room/removeRoom";

type ItemListProps = {
  id: number;
  name: string;
  code: string;
};

const StudyRoomsList: React.FC = () => {
  const router = useRouter();
  const [listItems, setListItems] = useState<RoomDataList>([]);

  useEffect(() => {
    GetList();
  }, []);

  const GetList = async () => {
    let response = await LoadRooms();
    if (response) setListItems(response);
  };

  const Delete = async (id: number) => {
    await RemoveRoom(id);
  };

  const ItemList = ({ id, name, code }: ItemListProps) => {
    return (
      <TouchableOpacity
        style={styles.itemList}
        onPress={() =>
          router.push({
            pathname: "/studyRoom",
            params: {
              roomId: id,
            },
          })
        }
      >
        <View>
          <Text
            style={{ ...TextStyles.labelBold, color: Colors["light"].primary }}
          >
            {name}
          </Text>
          <Text style={{ ...TextStyles.label }}>Sala: {code}</Text>
        </View>
        <View>
          <DeleteModal
            title={`Deletar Sala de Estudo`}
            text={name}
            onConfirm={() => Delete(id)}
            returnHref="/studyRoomsList"
          />
        </View>
      </TouchableOpacity>
    );
  };

  return (
    <ListScreenStructure
      title="Salas de Estudo"
      headerButton={
        <Button
          type="secondary"
          icon="plus"
          title="Criar"
          onPress={() => router.push("/createSturdyRoom")}
        />
      }
      listData={listItems}
      listRender={(item) => <ItemList {...item} />}
    />
  );
};

const styles = StyleSheet.create({
  itemList: {
    padding: 16,
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
    backgroundColor: Colors["light"].contentBackground,

    marginHorizontal: Spacing.m,
    marginBottom: Spacing.m,

    borderRadius: Spacing.m,
    borderColor: Colors["light"].primary,
    borderLeftWidth: 3,

    shadowColor: "#000",
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.23,
    shadowRadius: 2.62,

    elevation: 4,
  },
  button: {
    width: 60,
    height: 60,

    alignItems: "center",
    justifyContent: "center",
  },
});

export default StudyRoomsList;
