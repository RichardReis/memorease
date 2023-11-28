import React, { useState, useEffect } from "react";
import { useLocalSearchParams, useRouter } from "expo-router";
import { StyleSheet, Text, View } from "react-native";

import DeleteModal from "../components/DeleteModal";

import Spacing from "../constants/Spacing";
import Colors from "../constants/Colors";

import TextStyles from "../themedStyles/Text";
import ListScreenStructure from "../components/ListScreenStructure";
import {
  LoadUsers,
  UserRoomItem,
  UserRoomList,
} from "../service/Room/loadUsers";
import Icon from "../components/Icon";
import Button from "../components/Button";
import { RemoveUser } from "../service/Room/removeUser";

type ItemListProps = {
  id: number;
  name: string;
  code: string;
};

const RoomUsers: React.FC = () => {
  const router = useRouter();
  const params = useLocalSearchParams();
  const { roomId } = params;

  const [listItems, setListItems] = useState<UserRoomList>([]);

  useEffect(() => {
    Load();
  }, []);

  const Load = async () => {
    let response = await LoadUsers(parseInt(roomId as string));
    if (response) {
      setListItems(response);
    }
  };

  const Delete = async (email: string) => {
    await RemoveUser({
      email: email,
      roomId: parseInt(roomId as string),
    });
  };

  const ItemList = ({ id, name, email, firstName }: UserRoomItem) => {
    return (
      <View style={styles.itemList}>
        <View style={{ flexDirection: "row", gap: Spacing.g }}>
          <Icon color={Colors["light"].primary} name="account" />
          <Text
            style={{ ...TextStyles.labelBold, color: Colors["light"].primary }}
          >
            {name}
          </Text>
        </View>
        <View>
          <DeleteModal
            title={`Desvincular ${name} da Sala de Estudo`}
            text={name}
            onConfirm={() => Delete(email)}
            returnHref="/roomUsers"
          />
        </View>
      </View>
    );
  };

  return (
    <ListScreenStructure
      title="Usuários Vinculados"
      listData={listItems}
      headerButton={
        <Button
          type="secondary"
          icon="account-plus"
          title="Vincular"
          onPress={() => router.push("/addUserSturdyRoom")}
        />
      }
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
  clipboard: {
    flexDirection: "row",
    alignItems: "center",
  },
  content: {
    marginBottom: Spacing.g,
  },
});

export default RoomUsers;
